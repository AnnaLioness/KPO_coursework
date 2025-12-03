using Models.Models;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AdminService
    {
        private readonly IRepository<AdminModel> _repository;
        private readonly IRepository<DocumentModel> _documentRepository;

        public AdminService(IRepository<AdminModel> repository, IRepository<DocumentModel> documentRepository)
        {
            _repository = repository;
            _documentRepository = documentRepository;
        }

        // Регистрация нового администратора
        public async Task<AdminModel> RegisterAsync(string username, string password)
        {
            var existing = (await _repository.GetAllAsync()).FirstOrDefault(a => a.Username == username);
            if (existing != null)
                throw new Exception("Администратор с таким именем уже существует");

            var admin = new AdminModel
            {
                Username = username,
                PasswordHash = PasswordHelper.HashPassword(password)
            };

            await _repository.AddAsync(admin);
            await _repository.SaveAsync();
            return admin;
        }

        // Аутентификация администратора
        public async Task<AdminModel?> AuthenticateAsync(string username, string password)
        {
            var admin = (await _repository.GetAllAsync()).FirstOrDefault(a => a.Username == username);
            if (admin == null) return null;
            return PasswordHelper.VerifyPassword(password, admin.PasswordHash) ? admin : null;
        }

        // Получение всех админов
        public async Task<IEnumerable<AdminModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Получение админа по Id
        public async Task<AdminModel?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Обновление администратора
        public async Task UpdateAsync(AdminModel admin)
        {
            await _repository.UpdateAsync(admin);
            await _repository.SaveAsync();
        }

        // Удаление администратора
        public async Task DeleteAsync(AdminModel admin)
        {
            await _repository.DeleteAsync(admin);
            await _repository.SaveAsync();
        }
        public async Task DeleteAdminAsync(int adminId)
        {
            var admin = await _repository.GetByIdAsync(adminId);
            if (admin == null) return;

            // сначала удаляем документы
            var docs = await _documentRepository.GetAllAsync();
            foreach (var d in docs.Where(d => d.AdminId == adminId))
                await _documentRepository.DeleteAsync(d);

            await _documentRepository.SaveAsync();

            // удаляем админа
            await _repository.DeleteAsync(admin);
            await _repository.SaveAsync();
        }
    }
}
