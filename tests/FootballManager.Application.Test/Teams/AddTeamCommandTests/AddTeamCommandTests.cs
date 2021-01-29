using FootballManager.Application.Teams.Commands;
using FootballManager.Persistence.Repositories.TeamRepository;
using FootballManager.Persistence.Repositories.PlayerRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Teams.Commands.AddTeamCommand;

namespace FootballManager.Application.Test.Teams.AddTeamCommandTests
{
    public class AddTeamCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task AddTeamCommand_Can_Add_Team()
        {
            using (var dbContext = GetDbContext("AddTeamCommand_Can_Add_Team"))
            {
                var fakeRepo = new PlayerRepository(dbContext);
                await fakeRepo.AddAsync(new Domain.Entities.Player
                {
                    Name = "FirstPlayer",
                    Surname = "LastName",
                    Height = 1.98,
                    EmailAddress = "test@email.com"
                });
                await fakeRepo.AddAsync(new Domain.Entities.Player
                {
                    Name = "SecondPlayer",
                    Surname = "LastName",
                    Height = 1.98,
                    EmailAddress = "test@email.com"
                });
            }

            using (var dbContext = GetDbContext("AddTeamCommand_Can_Add_Team"))
            {
                var fakeTeamRepo = new TeamRepository(dbContext);
                var fakePlayerRepo = new PlayerRepository(dbContext);
                var fakeLogger = new Mock<ILogger<AddTeamCommandHandler>>();
                var handler = new AddTeamCommandHandler(fakeTeamRepo, fakePlayerRepo, GetMapper(), fakeLogger.Object);

                var command = new AddTeamCommand
                {
                    Name = "TestTeamName",
                    Longitude = 8.11,
                    Latitude = 9.43,
                    PlayerIds = new List<int> { 1, 2 }
                };

                var result = await handler.Handle(command, default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(command.Name, result.TeamLookupModel.Name);
                Assert.Equal(command.Latitude, result.TeamLookupModel.Latitude);
                Assert.Equal(command.Longitude, result.TeamLookupModel.Longitude);
                Assert.Equal(command.PlayerIds.ToList()[0], result.TeamLookupModel.Players.ToList()[0].Id);
                Assert.Equal(command.PlayerIds.ToList()[1], result.TeamLookupModel.Players.ToList()[1].Id);
            }
        }
    }
}
