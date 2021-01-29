using FootballManager.Domain.Entities;
using System.Threading.Tasks;

namespace FootballManager.Persistence.Repositories.TeamRepository
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<Team> GetTeamByIdAsync(long id);
    }
}
