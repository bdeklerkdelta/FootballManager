using FootballManager.Application.Teams.Commands;
using FootballManager.Persistence.Repositories.TeamRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Teams.Commands.DeleteTeamCommand;

namespace FootballManager.Application.Test.Teams.AddTeamCommandTests
{
    public class DeleteTeamCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task DeleteTeamCommand_Can_Delete_Team()
        {
            using (var dbContext = GetDbContext("DeleteTeamCommand_Can_Delete_Team"))
            {
                var fakeRepo = new TeamRepository(dbContext);
                await fakeRepo.AddAsync(new Domain.Entities.Team
                {
                    Name = "TestTeamName"
                });
            }

            using (var dbContext = GetDbContext("DeleteTeamCommand_Can_Delete_Team"))
            {
                var fakeRepo = new TeamRepository(dbContext);
                var fakeLogger = new Mock<ILogger<DeleteTeamCommandHandler>>();
                var handler = new DeleteTeamCommandHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var command = new DeleteTeamCommand
                {
                    Id = 1,
                };

                var result = await handler.Handle(command, default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(command.Id, result.TeamDeleteModel.Id);
                Assert.Equal(Domain.Enumerations.EnumBag.DataState.Inactive, result.TeamDeleteModel.DataState);
            }
        }
    }
}
