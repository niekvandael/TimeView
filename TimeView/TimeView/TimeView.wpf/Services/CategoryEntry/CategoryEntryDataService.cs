using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;
using TimeView.domain;

namespace TimeView.wpf.Services
{
    public class CategoryEntryDataService : ICategoryEntryDataService
    {
        ICategoryEntryRepository repository;

        public CategoryEntryDataService(ICategoryEntryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CategoryEntry[]> GetCategoryEntriesForCompany(int CompanyId)
        {
            return await repository.getCategoryEntriesForCompany(CompanyId);
        }
    }
}