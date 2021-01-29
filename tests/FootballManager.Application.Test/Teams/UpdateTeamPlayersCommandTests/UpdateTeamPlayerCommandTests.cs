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
using static FootballManager.Application.Teams.Commands.UpdateTeamPlayersCommand;
using FootballManager.Domain.Entities;

namespace FootballManager.Application.Test.Teams.UpdateTeamPlayersCommandTests
{
    public class UpdateTeamPlayersCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task UpdateTeamPlayersCommand_Can_Add_Team()
        {
            using (var dbContext = GetDbContext("UpdateTeamPlayersCommand_Can_Add_Team"))
            {
                var fakePlayerRepo = new PlayerRepository(dbContext);
                await fakePlayerRepo.AddAsync(new Domain.Entities.Player
                {
                    Name = "FirstPlayer",
                    Surname = "LastName",
                    Height = 1.98,
                    EmailAddress = "test@email.com"
                });
                await fakePlayerRepo.AddAsync(new Domain.Entities.Player
                {
                    Name = "SecondPlayer",
                    Surname = "LastName",
                    Height = 1.98,
                    EmailAddress = "test@email.com"
                });

                var fakeTeamRepo = new TeamRepository(dbContext);

                await fakeTeamRepo.AddAsync(new Domain.Entities.Team
                {
                    Name = "TeamName",
                    Longitude = 1.11,
                    Latitude = 1.43,
                });

                var fakeTeam = fakeTeamRepo.GetTeamByIdAsync(1).Result;

                var fakePlayerOne = fakePlayerRepo.GetPlayerByIdAsync(1).Result;

                fakePlayerOne.Team = fakeTeam;
                fakeTeam.Players = new List<Player> { fakePlayerOne };

                await fakePlayerRepo.UpdateAsync(fakePlayerOne);
                await fakeTeamRepo.UpdateAsync(fakeTeam);

                var fakeLogger = new Mock<ILogger<UpdateTeamPlayersCommandHandler>>();
                var handler = new UpdateTeamPlayersCommandHandler(fakeTeamRepo, fakePlayerRepo, GetMapper(), fakeLogger.Object);

                var command = new UpdateTeamPlayersCommand
                {
                    Id = 1,
                    PlayerIds = new List<int> { 2 }
                };

                var result = await handler.Handle(command, default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(command.Id, result.TeamLookupModel.Id);
                Assert.Equal(1, result.TeamLookupModel.Players.ToList()[0].Id);
                Assert.Equal(command.PlayerIds.ToList()[0], result.TeamLookupModel.Players.ToList()[1].Id);
            }
        }
    }
}
