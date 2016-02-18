using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.wpf.Services
{
    public class CategoryEntryDataService : ICategoryEntryDataService
    {
        private readonly ICategoryEntryRepository _repository;

        public CategoryEntryDataService(ICategoryEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryEntry[]> GetCategoryEntriesForCompany(int companyId)
        {
            return await _repository.GetCategoryEntriesForCompany(companyId);
        }
    }
}