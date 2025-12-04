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

namespace Test.Tests
{
    public class DocumentServiceTests
    {
        private (AdminService adminService,DocumentService documentService) CreateServices()
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
        // ADD
        // --------------------------------------------------------

        [Fact]
        public async Task AddAsync_ShouldAddDocument() {
            var (adminService, documentService) = CreateServices();

            // 1) создаём админа
            var admin = await adminService.RegisterAsync("admin", "pass");

            // 2) создаём документы через сервис с заполнением всех обязательных полей
            await documentService.AddAsync(new DocumentModel
            {
                AdminId = admin.Id,
                Title = "Doc",
                Author = "Author1",
                Year = "2025",
                Content = "Content1",
                CloudLink = "https://cloud.test/doc1"
            });

            var all = (await documentService.GetAllAsync()).ToList(); 
            Assert.Single(all); 
            Assert.Equal("Doc", all[0].Title); 
            Assert.Equal(1, all[0].AdminId); }

        // --------------------------------------------------------
        // GET ALL
        // --------------------------------------------------------

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllDocuments()
        {
            var (adminService, documentService) = CreateServices();

            var admin = await adminService.RegisterAsync("admin", "pass");

            await documentService.AddAsync(new DocumentModel { AdminId = admin.Id, Title = "D1", Author = "A1", Year = "2025", Content = "C1", CloudLink = "link1" });
            await documentService.AddAsync(new DocumentModel { AdminId = admin.Id, Title = "D2", Author = "A2", Year = "2025", Content = "C2", CloudLink = "link2" });

            var all = (await documentService.GetAllAsync()).ToList();
            Assert.Equal(2, all.Count);
            Assert.Contains(all, d => d.Title == "D1");
            Assert.Contains(all, d => d.Title == "D2");
        }
        // --------------------------------------------------------
        // GET BY ID
        // --------------------------------------------------------

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectDocument()
        {
            var (adminService, documentService) = CreateServices();

            var admin = await adminService.RegisterAsync("admin", "pass");

            var doc = await documentService.AddAsync(new DocumentModel
            {
                AdminId = admin.Id,
                Title = "D",
                Author = "Author",
                Year = "2025",
                Content = "Content",
                CloudLink = "link"
            });

            var loaded = await documentService.GetByIdAsync(doc.Id);
            Assert.NotNull(loaded);
            Assert.Equal("D", loaded.Title);
            Assert.Equal(admin.Id, loaded.AdminId);
        }

        // --------------------------------------------------------
        // UPDATE
        // --------------------------------------------------------

        [Fact]
        public async Task UpdateAsync_ShouldUpdateDocument()
        {
            var (adminService, documentService) = CreateServices();

            var admin = await adminService.RegisterAsync("admin", "pass");

            var doc = await documentService.AddAsync(new DocumentModel
            {
                AdminId = admin.Id,
                Title = "Old",
                Author = "Author",
                Year = "2025",
                Content = "Content",
                CloudLink = "link"
            });

            doc.Title = "New";
            await documentService.UpdateAsync(doc);

            var loaded = await documentService.GetByIdAsync(doc.Id);
            Assert.Equal("New", loaded.Title);
        }        // --------------------------------------------------------
        // DELETE
        // --------------------------------------------------------

        [Fact]
        public async Task DeleteAsync_ShouldDeleteDocument()
        {
            var (adminService, documentService) = CreateServices();

            var admin = await adminService.RegisterAsync("admin", "pass");

            var doc = await documentService.AddAsync(new DocumentModel
            {
                AdminId = admin.Id,
                Title = "Doc",
                Author = "Author",
                Year = "2025",
                Content = "Content",
                CloudLink = "link"
            });

            await documentService.DeleteAsync(doc);

            var all = (await documentService.GetAllAsync()).ToList();
            Assert.Empty(all);
        }
        // --------------------------------------------------------
        // GET BY ADMIN ID
        // --------------------------------------------------------

        [Fact]
        public async Task GetDocumentsByAdminAsync_ShouldReturnOnlyAdminsDocs()
        {
            var (adminService, documentService) = CreateServices();

            var admin1 = await adminService.RegisterAsync("admin1", "pass1");
            var admin2 = await adminService.RegisterAsync("admin2", "pass2");

            await documentService.AddAsync(new DocumentModel { AdminId = admin1.Id, Title = "D1", Author = "A1", Year = "2025", Content = "C1", CloudLink = "link1" });
            await documentService.AddAsync(new DocumentModel { AdminId = admin2.Id, Title = "D2", Author = "A2", Year = "2025", Content = "C2", CloudLink = "link2" });
            await documentService.AddAsync(new DocumentModel { AdminId = admin1.Id, Title = "D3", Author = "A3", Year = "2025", Content = "C3", CloudLink = "link3" });

            var list = (await documentService.GetDocumentsByAdminAsync(admin1.Id)).ToList();
            Assert.Equal(2, list.Count);
            Assert.All(list, d => Assert.Equal(admin1.Id, d.AdminId));
            Assert.Contains(list, d => d.Title == "D1");
            Assert.Contains(list, d => d.Title == "D3");
        }
    }
}
