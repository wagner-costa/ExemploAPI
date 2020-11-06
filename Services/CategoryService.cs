using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Exemplo.API.Domain.Models;
using Exemplo.API.Domain.Repositories;
using Exemplo.API.Domain.Services;
using Exemplo.API.Domain.Services.Communication;
using Exemplo.API.Infrastructure;

namespace Exemplo.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            var categories = await _cache.GetOrCreateAsync(CacheKeys.CategoriesList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _categoryRepository.ListAsync();
            });
            
            return categories;
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(category);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"Ocorreu um erro ao salvar a categoria: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoryResponse("Categoria não encontrada.");

            existingCategory.Name = category.Name;

            try
            {
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new CategoryResponse($"Ocorreu um erro ao atualizar a categoria: {ex.Message}");
            }
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            var existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new CategoryResponse("Categoria não encontrada.");

            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new CategoryResponse(existingCategory);
            }
            catch (Exception ex)
            {
                return new CategoryResponse($"Ocorreu um erro ao excluir a categoria: {ex.Message}");
            }
        }
    }
}
