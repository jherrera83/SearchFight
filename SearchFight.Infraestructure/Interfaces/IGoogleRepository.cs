using SearchFight.Infraestructure.Entities;
using System.Threading.Tasks;

namespace SearchFight.Infraestructure.Interfaces
{
    public interface IGoogleRepository
    {
        Task<GoogleResponse> Search(string keyword);
    }
}
