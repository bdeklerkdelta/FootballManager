using FootballManager.Domain.Entities;
using FootballManager.Persistence.Repositories.StadiumRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FootballManager.Persistence.Repositories.StadiumRepository
{
    public class StadiumRepository : Repository<Stadium>, IStadiumRepository
    {
        public StadiumRepository(FootballManagerDbContext footballManagerDbContext) : base(footballManagerDbContext)
        {
        }

        public Task<Stadium> GetStadiumByIdAsync(long id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
