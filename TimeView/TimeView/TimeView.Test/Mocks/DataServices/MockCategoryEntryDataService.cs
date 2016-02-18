using System.Threading.Tasks;
using TimeView.data;
using TimeView.Test.Mocks.Repositories;
using TimeView.wpf.Services;

namespace TimeView.Test.Mocks.DataServices
{
    internal class MockCategoryEntryDataService : ICategoryEntryDataService
    {
        private readonly MockCategoryEntryRepository _repository = new MockCategoryEntryRepository();

        public Task<CategoryEntry[]> GetCategoryEntriesForCompany(int companyId)
        {
            return _repository.GetCategoryEntriesForCompany(companyId);
        }
    }
}