using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.Test.Mocks.Repositories;
using TimeView.wpf.Services;

namespace TimeView.Test.Mocks.DataServices
{
    class MockCategoryEntryDataService : ICategoryEntryDataService
    {
        private MockCategoryEntryRepository repository = new MockCategoryEntryRepository();

        public Task<CategoryEntry[]> GetCategoryEntriesForCompany(int CompanyId)
        {
            return repository.getCategoryEntriesForCompany(CompanyId);
        }
    }
}
