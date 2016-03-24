using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.DAL.Repositories.Category
{
    public interface ICategoryEntryRepository
    {
        Task<CategoryEntry[]> GetCategoryEntries(int categoryId);
        Task<bool> CreateCategoryEntry(CategoryEntry categoryEntry);
        Task<bool> DeleteCategoryEntry(CategoryEntry categoryEntry);
        Task<CategoryEntry> UpdateCategoryEntry(CategoryEntry categoryEntry);
    }
}