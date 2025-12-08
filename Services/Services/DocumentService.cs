using Models.DTOs;
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
    public class DocumentService
    {
        private readonly IRepository<DocumentModel> _repository;

        public DocumentService(IRepository<DocumentModel> repository)
        {
            _repository = repository;
        }

        // Добавление документа
        public async Task<DocumentModel> AddAsync(DocumentModel document)
        {
            await _repository.AddAsync(document);
            await _repository.SaveAsync();
            return document;
        }

        // Получение всех документов
        public async Task<IEnumerable<DocumentModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Получение документа по Id
        public async Task<DocumentModel?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Обновление документа
        public async Task UpdateAsync(DocumentModel document)
        {
            await _repository.UpdateAsync(document);
            await _repository.SaveAsync();
        }

        // Удаление документа
        public async Task DeleteAsync(DocumentModel document)
        {
            await _repository.DeleteAsync(document);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<DocumentModel>> GetDocumentsByAdminAsync(int adminId)
        {
            var all = await _repository.GetAllAsync();
            return all.Where(d => d.AdminId == adminId);
        }
        public async Task<List<DocumentDTO>> SearchAsync(string query)
        {
            var allDocs = await _repository.GetAllAsync();

            var dtos = allDocs.Select(d => new DocumentDTO
            {
                Id = d.Id,
                AdminId = d.AdminId,
                Title = d.Title,
                Author = d.Author,
                Year = d.Year,
                CloudLink = d.CloudLink,
                ContentForSearch = d.Content,
                HighlightedContent = ""
            }).ToList();

            if (string.IsNullOrWhiteSpace(query)) return dtos;

            var queryLemmas = MorphologyHelper.GetLemmas(query).ToHashSet();

            foreach (var dto in dtos)
            {
                var docLemmas = MorphologyHelper.GetLemmas(dto.ContentForSearch);
                if (docLemmas.Any(l => queryLemmas.Contains(l)))
                {
                    dto.HighlightedContent = MorphologyHelper.HighlightText(dto.ContentForSearch, query);
                }
                else
                {
                    dto.HighlightedContent = "";
                }
            }

            return dtos.Where(d => !string.IsNullOrEmpty(d.HighlightedContent)).ToList();
        }
    }
}
