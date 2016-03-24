using System;
using System.Threading.Tasks;
using TimeView.DAL.Repositories.Category;
using TimeView.data;

namespace TimeView.wpf.Services
{
    public class CategoryEntryDataService : ICategoryEntryDataService
    {
        private readonly ICategoryEntryRepository _repository;

        public CategoryEntryDataService(ICategoryEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateCategoryEntry(CategoryEntry categoryEntry)
        {
            return await _repository.CreateCategoryEntry(categoryEntry);
        }

        public async Task<bool> DeleteCategoryEntry(CategoryEntry categoryEntry)
        {
            return await _repository.DeleteCategoryEntry(categoryEntry);
        }

        public async Task<CategoryEntry[]> GetCategoryEntries(int companyId)
        {
            return await _repository.GetCategoryEntries(companyId);
        }

        public async Task<CategoryEntry> UpdateCategoryEntry(CategoryEntry categoryEntry)
        {
            return await _repository.UpdateCategoryEntry(categoryEntry);
        }
    }
}