using FootballManager.Application.Teams.Commands;
using FootballManager.Persistence.Repositories.TeamRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Teams.Commands.UpdateTeamCommand;

namespace FootballManager.Application.Test.Teams.AddTeamCommandTests
{
    public class UpdateTeamCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task UpdateTeamCommand_Can_Update_Team()
        {
            using (var dbContext = GetDbContext("UpdateTeamCommand_Can_Update_Team"))
            {
                var fakeRepo = new TeamRepository(dbContext);
                await fakeRepo.AddAsync(new Domain.Entities.Team
                {
                    Name = "TeamName",
                    Longitude = 8.11,
                    Latitude = 9.43,
                });
            }

            using (var dbContext = GetDbContext("UpdateTeamCommand_Can_Update_Team"))
            {
                var fakeRepo = new TeamRepository(dbContext);
                var fakeLogger = new Mock<ILogger<UpdateTeamCommandHandler>>();
                var handler = new UpdateTeamCommandHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var command = new UpdateTeamCommand
                {
                    Id = 1,
                    Name = "NewTeamName",
                    Longitude = 2.11,
                    Latitude = 6.43,
                };

                var result = await handler.Handle(command, default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(command.Name, result.TeamLookupModel.Name);
                Assert.Equal(command.Longitude, result.TeamLookupModel.Longitude);
                Assert.Equal(command.Latitude, result.TeamLookupModel.Latitude);
            }
        }
    }
}
