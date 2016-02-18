using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.Test.Mocks.Repositories
{
    class MockCategoryEntryRepository : ICategoryEntryRepository
    {
        public Task<CategoryEntry[]> getCategoryEntriesForCompany(int CompanyId)
        {
            List<CategoryEntry> categoryEntries = new List<CategoryEntry>();

            CategoryEntry categoryEntry1 = new CategoryEntry
            {
                Id = 1,
                CategoryId = 1,
                Start = DateTime.Parse("09:00"),
                End = DateTime.Parse("17:00"),
                Name = "gray"
            };

            CategoryEntry categoryEntry2 = new CategoryEntry
            {
                Id = 2,
                CategoryId = 1,
                Start = DateTime.Parse("14:00"),
                End = DateTime.Parse("22:00"),
                Name = "gray"
            };

            categoryEntries.Add(categoryEntry1);
            categoryEntries.Add(categoryEntry2);

            return Task.FromResult(categoryEntries.ToArray());
        }

    }
}