using FootballManager.Domain.Entities;
using System.Threading.Tasks;

namespace FootballManager.Persistence.Repositories.PlayerRepository
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetPlayerByIdAsync(long id);
    }
}
