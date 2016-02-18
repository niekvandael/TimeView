using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.domain
{
    public interface ICategoryEntryRepository
    {
        Task<CategoryEntry[]> GetCategoryEntriesForCompany(int companyId);
    }
}