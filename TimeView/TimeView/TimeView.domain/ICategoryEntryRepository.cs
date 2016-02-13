using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.domain
{
    public interface ICategoryEntryRepository
    {
        System.Threading.Tasks.Task<CategoryEntry[]> getCategoryEntriesForCompany(int CompanyId);
    }
}
