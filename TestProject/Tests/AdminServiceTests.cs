using Microsoft.EntityFrameworkCore;
using Models.Models;
using Repositories.Implementations;
using Repositories;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Tests
{
    public class AdminServiceTests
    {
        private (AdminService adminService, DocumentService documentService) CreateServices()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            var adminRepo = new AdminRepository(context);
            var docRepo = new DocumentRepository(context);

            var adminService = new AdminService(adminRepo, docRepo);
            var documentService = new DocumentService(docRepo);

            return (adminService, documentService);
        }

        // --------------------------------------------------------
        // REGISTRATION
        // --------------------------------------------------------

        [Fact]
        public async Task RegisterAsync_ShouldRegisterNewAdmin()
        {
            var (adminService, _) = CreateServices();

            var admin = await adminService.RegisterAsync("testAdmin", "123");

            Assert.NotNull(admin);
            Assert.True(admin.Id > 0);
            Assert.Equal("testAdmin", admin.Username);
            Assert.NotEmpty(admin.PasswordHash);
        }

        [Fact]
        public async Task RegisterAsync_ShouldThrow_WhenUsernameExists()
        {
            var (adminService, _) = CreateServices();

            await adminService.RegisterAsync("admin", "123");

            await Assert.ThrowsAsync<System.Exception>(async () =>
            {
                await adminService.RegisterAsync("admin", "321");
            });
        }

        // --------------------------------------------------------
        // AUTHENTICATION
        // --------------------------------------------------------

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnAdmin_WhenPasswordCorrect()
        {
            var (adminService, _) = CreateServices();

            await adminService.RegisterAsync("anna", "12345");

            var admin = await adminService.AuthenticateAsync("anna", "12345");

            Assert.NotNull(admin);
            Assert.Equal("anna", admin.Username);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnNull_WhenPasswordIncorrect()
        {
            var (adminService, _) = CreateServices();

            await adminService.RegisterAsync("anna", "12345");

            var admin = await adminService.AuthenticateAsync("anna", "wrong");

            Assert.Null(admin);
        }

        // --------------------------------------------------------
        // CASCADE DELETE
        // --------------------------------------------------------

        [Fact]
        public async Task DeleteAdminAsync_ShouldDeleteAdminAndAllHisDocuments()
        {
            var (adminService, documentService) = CreateServices();

            // 1) создаём админа
            var admin = await adminService.RegisterAsync("admin", "pass");

            // 2) создаём документы через сервис с заполнением всех обязательных полей
            await documentService.AddAsync(new DocumentModel
            {
                AdminId = admin.Id,
                Title = "D1",
                Author = "Author1",
                Year = "2025",
                Content = "Content1",
                CloudLink = "https://cloud.test/doc1"
            });

            await documentService.AddAsync(new DocumentModel
            {
                AdminId = admin.Id,
                Title = "D2",
                Author = "Author2",
                Year = "2025",
                Content = "Content2",
                CloudLink = "https://cloud.test/doc2"
            });

            // Проверяем, что документы добавились
            Assert.Equal(2, (await documentService.GetAllAsync()).Count());

            // 3) вызываем каскадное удаление
            await adminService.DeleteAdminAsync(admin.Id);

            // 4) проверяем, что админ удалён
            Assert.Null(await adminService.GetByIdAsync(admin.Id));

            // 5) проверяем, что все документы админа удалены
            Assert.Empty(await documentService.GetAllAsync());
        }
    }
}
