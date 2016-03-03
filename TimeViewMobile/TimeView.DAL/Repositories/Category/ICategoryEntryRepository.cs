using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.DAL.Repositories.Category
{
    public interface ICategoryEntryRepository
    {
        Task<CategoryEntry[]> GetCategoryEntriesForCompany(int companyId);
    }
}