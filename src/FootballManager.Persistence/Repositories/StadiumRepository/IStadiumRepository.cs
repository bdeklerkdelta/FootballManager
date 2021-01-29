using FootballManager.Domain.Entities;
using System.Threading.Tasks;

namespace FootballManager.Persistence.Repositories.StadiumRepository
{
    public interface IStadiumRepository : IRepository<Stadium>
    {
        Task<Stadium> GetStadiumByIdAsync(long id);
    }
}
