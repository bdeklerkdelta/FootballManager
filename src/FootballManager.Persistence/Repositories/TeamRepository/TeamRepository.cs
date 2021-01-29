using FootballManager.Domain.Entities;
using FootballManager.Persistence.Repositories.TeamRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FootballManager.Persistence.Repositories.TeamRepository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(FootballManagerDbContext footballManagerDbContext) : base(footballManagerDbContext)
        {
        }

        public Task<Team> GetTeamByIdAsync(long id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
