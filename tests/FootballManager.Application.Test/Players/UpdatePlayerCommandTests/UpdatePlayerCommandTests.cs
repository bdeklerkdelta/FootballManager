using FootballManager.Application.Players.Commands;
using FootballManager.Persistence.Repositories.PlayerRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Players.Commands.UpdatePlayerCommand;

namespace FootballManager.Application.Test.Players.AddPlayerCommandTests
{
    public class UpdatePlayerCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task UpdatePlayerCommand_Can_Update_Player()
        {
            using (var dbContext = GetDbContext("UpdatePlayerCommand_Can_Update_Player"))
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

            using (var dbContext = GetDbContext("UpdatePlayerCommand_Can_Update_Player"))
            {
                var fakeRepo = new PlayerRepository(dbContext);
                var fakeLogger = new Mock<ILogger<UpdatePlayerCommandHandler>>();
                var handler = new UpdatePlayerCommandHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var command = new UpdatePlayerCommand
                {
                    Id = 1,
                    Name = "NewName",
                    Surname = "NewLastName",
                    Height = 1.55,
                    EmailAddress = "newtest@email.com"
                };

                var result = await handler.Handle(command, default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(command.Name, result.PlayerLookupModel.Name);
                Assert.Equal(command.Surname, result.PlayerLookupModel.Surname);
                Assert.Equal(command.Height, result.PlayerLookupModel.Height);
                Assert.Equal(command.EmailAddress, result.PlayerLookupModel.EmailAddress);
            }
        }
    }
}
