using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.wpf.Services
{
    public interface ICategoryEntryDataService
    {
        Task<CategoryEntry[]> GetCategoryEntries(int categoryEntryId);
        Task<bool> CreateCategoryEntry(CategoryEntry categoryEntry);
        Task<bool> DeleteCategoryEntry(CategoryEntry categoryEntry);
        Task<CategoryEntry> UpdateCategoryEntry(CategoryEntry categoryEntry);
    }
}