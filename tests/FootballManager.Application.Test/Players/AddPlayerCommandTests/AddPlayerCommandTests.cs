using FootballManager.Application.Players.Commands;
using FootballManager.Persistence.Repositories.PlayerRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Players.Commands.AddPlayerCommand;

namespace FootballManager.Application.Test.Players.AddPlayerCommandTests
{
    public class AddPlayerCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task AddPlayerCommand_Can_Add_Player()
        {
            var dbContext = GetDbContext("AddPlayerCommand_Can_Add_Player");

            using (dbContext)
            {
                var fakeRepo = new PlayerRepository(dbContext);
                var fakeLogger = new Mock<ILogger<AddPlayerCommandHandler>>();
                var handler = new AddPlayerCommandHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var command = new AddPlayerCommand
                {
                    Name = "FirstName",
                    Surname = "LastName",
                    Height = 1.98,
                    EmailAddress = "test@email.com"
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
