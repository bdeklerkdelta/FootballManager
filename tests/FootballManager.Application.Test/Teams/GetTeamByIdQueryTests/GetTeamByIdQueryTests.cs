using FootballManager.Application.Teams.Commands;
using FootballManager.Application.Teams.Queries;
using FootballManager.Domain.Entities;
using FootballManager.Persistence.Repositories.TeamRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Teams.Queries.GetTeamByIdQuery;

namespace FootballManager.Application.Test.Teams.GetTeamByIdQueryTests
{
    public class GetTeamByIdQueryTests : BaseHandlerTest
    {
        [Fact]
        public async Task GetTeamByIdQuery_Can_Return_Team()
        {
            var dbContext = GetDbContext("GetTeamByIdQuery_Can_Return_Team");

            using (dbContext)
            {
                var fakeRepo = new TeamRepository(dbContext);
                var fakeLogger = new Mock<ILogger<GetTeamByIdQueryHandler>>();
                var handler = new GetTeamByIdQueryHandler(fakeRepo, GetMapper(), fakeLogger.Object);

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
                    await fakeRepo.AddAsync(TeamToAdd);
                }

                var result = await handler.Handle(new GetTeamByIdQuery()
                {
                    Id = 2
                }, default);

                Assert.False(result.Notifications.HasErrors());

                var secondTeam = result.TeamLookupModel;

                Assert.Equal(TeamsToAdd[1].Name, secondTeam.Name);
                Assert.Equal(TeamsToAdd[1].Longitude, secondTeam.Longitude);
                Assert.Equal(TeamsToAdd[1].Latitude, secondTeam.Latitude);
            }
        }
    }
}
