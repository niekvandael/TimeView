using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.DAL.Repositories.OpenData
{
    public interface IOpenDataRepository
    {
        Task<Company[]> UpdateOpenData();
    }
}