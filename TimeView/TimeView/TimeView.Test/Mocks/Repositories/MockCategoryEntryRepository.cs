using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.Test.Mocks.Repositories
{
    internal class MockCategoryEntryRepository : ICategoryEntryRepository
    {
        public Task<CategoryEntry[]> GetCategoryEntriesForCompany(int companyId)
        {
            var categoryEntries = new List<CategoryEntry>();

            var categoryEntry1 = new CategoryEntry
            {
                Id = 1,
                CategoryId = 1,
                Start = DateTime.Parse("09:00"),
                End = DateTime.Parse("17:00"),
                Name = "gray"
            };

            var categoryEntry2 = new CategoryEntry
            {
                Id = 2,
                CategoryId = 1,
                Start = DateTime.Parse("14:00"),
                End = DateTime.Parse("22:00"),
                Name = "green"
            };

            categoryEntries.Add(categoryEntry1);
            categoryEntries.Add(categoryEntry2);

            return Task.FromResult(categoryEntries.ToArray());
        }
    }
}