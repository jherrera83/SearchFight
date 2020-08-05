using SearchFight.Infraestructure.Entities;
using System.Threading.Tasks;

namespace SearchFight.Infraestructure.Interfaces
{
    public interface IBingRepository
    {
        Task<BingResponse> Search(string keyword);
    }
}