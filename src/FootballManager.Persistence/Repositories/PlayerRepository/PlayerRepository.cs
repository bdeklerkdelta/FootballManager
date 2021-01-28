using FootballManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FootballManager.Persistence.Repositories.PlayerRepository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(FootballManagerDbContext footballManagerDbContext) : base(footballManagerDbContext)
        {
        }

        public Task<Player> GetPlayerByIdAsync(long id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
