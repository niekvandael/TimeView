using System.Threading.Tasks;
using TimeView.data;

namespace TimeView.domain
{
    public interface IOpenDataRepository
    {
        Task<Company[]> UpdateOpenData();
    }
}