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
using static FootballManager.Application.Teams.Queries.GetTeamsQuery;

namespace FootballManager.Application.Test.Teams.GetTeamsQueryTests
{
    public class GetTeamsQueryTests : BaseHandlerTest
    {
        [Fact]
        public async Task GetTeamsQuery_Can_Return_Teams()
        {
            var dbContext = GetDbContext("GetTeamsQuery_Can_Return_Teams");

            using (dbContext)
            {
                var fakeRepo = new TeamRepository(dbContext);
                var fakeLogger = new Mock<ILogger<GetTeamsQueryHandler>>();
                var handler = new GetTeamsQueryHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var TeamToAdd = new Team
                {
                    Name = "TeamName",
                    Longitude = 8.11,
                    Latitude = 9.43,
                };

                await fakeRepo.AddAsync(TeamToAdd);

                var result = await handler.Handle(new GetTeamsQuery(), default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(1, result.Teams.Count);
                Assert.Equal(TeamToAdd.Name, result.Teams[0].Name);
                Assert.Equal(TeamToAdd.Longitude, result.Teams[0].Longitude);
                Assert.Equal(TeamToAdd.Longitude, result.Teams[0].Longitude);
            }
        }
    }
}
