using FootballManager.Application.Players.Commands;
using FootballManager.Persistence.Repositories.PlayerRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Players.Commands.DeletePlayerCommand;

namespace FootballManager.Application.Test.Players.AddPlayerCommandTests
{
    public class DeletePlayerCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task DeletePlayerCommand_Can_Delete_Player()
        {
            using (var dbContext = GetDbContext("DeletePlayerCommand_Can_Delete_Player"))
            {
                var fakeRepo = new PlayerRepository(dbContext);
                await fakeRepo.AddAsync(new Domain.Entities.Player
                {
                    Name = "FirstName",
                    Surname = "LastName",
                    Height = 1.98,
                    EmailAddress = "test@email.com"
                });
            }

            using (var dbContext = GetDbContext("DeletePlayerCommand_Can_Delete_Player"))
            {
                var fakeRepo = new PlayerRepository(dbContext);
                var fakeLogger = new Mock<ILogger<DeletePlayerCommandHandler>>();
                var handler = new DeletePlayerCommandHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var command = new DeletePlayerCommand
                {
                    Id = 1,
                };

                var result = await handler.Handle(command, default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(command.Id, result.PlayerDeleteModel.Id);
                Assert.Equal(Domain.Enumerations.EnumBag.DataState.Inactive, result.PlayerDeleteModel.DataState);
            }
        }
    }
}
