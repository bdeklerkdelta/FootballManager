using FootballManager.Application.Stadiums.Commands;
using FootballManager.Application.Stadiums.Queries;
using FootballManager.Domain.Entities;
using FootballManager.Persistence.Repositories.TeamRepository;
using FootballManager.Persistence.Repositories.StadiumRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Stadiums.Queries.GetStadiumsQuery;

namespace FootballManager.Application.Test.Stadiums.GetStadiumsQueryTests
{
    public class GetStadiumsQueryTests : BaseHandlerTest
    {
        [Fact]
        public async Task GetStadiumsQuery_Can_Return_Stadiums()
        {
            var dbContext = GetDbContext("GetStadiumsQuery_Can_Return_Stadiums");

            using (dbContext)
            {
                var fakeStadiumRepo = new StadiumRepository(dbContext);
                var fakeTeamRepo = new TeamRepository(dbContext);
                var fakeLogger = new Mock<ILogger<GetStadiumsQueryHandler>>();
                var handler = new GetStadiumsQueryHandler(fakeStadiumRepo, GetMapper(), fakeLogger.Object);

                var TeamsToAdd = new List<Team>
                {
                    new Team
                    {
                        Name = "TeamOne",
                        Longitude = 1.11,
                        Latitude = 1.43,
                    },
                    new Team
                    {
                        Name = "TeamTwo",
                        Longitude = 2.11,
                        Latitude = 2.43,},
                    new Team
                    {
                        Name = "TeamThree",
                        Longitude = 3.11,
                        Latitude = 3.43,},
                };

                foreach (var TeamToAdd in TeamsToAdd)
                {
                    await fakeTeamRepo.AddAsync(TeamToAdd);
                }

                var StadiumToAdd = new Stadium
                {
                    Name = "StadiumName",
                    TeamId = 2
                };

                await fakeStadiumRepo.AddAsync(StadiumToAdd);

                var result = await handler.Handle(new GetStadiumsQuery(), default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(1, result.Stadiums.Count);
                Assert.Equal(StadiumToAdd.Name, result.Stadiums[0].Name);
                Assert.Equal(StadiumToAdd.TeamId, result.Stadiums[0].Team.Id);
            }
        }
    }
}
