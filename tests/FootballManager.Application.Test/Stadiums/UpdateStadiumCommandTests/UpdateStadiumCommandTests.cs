using FootballManager.Application.Stadiums.Commands;
using FootballManager.Persistence.Repositories.TeamRepository;
using FootballManager.Persistence.Repositories.PlayerRepository;
using FootballManager.Persistence.Repositories.StadiumRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Stadiums.Commands.UpdateStadiumCommand;
using FootballManager.Domain.Entities;
using System.Collections.Generic;

namespace FootballManager.Application.Test.Stadiums.AddStadiumCommandTests
{
    public class UpdateStadiumCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task UpdateStadiumCommand_Can_Update_Stadium()
        {
            using (var dbContext = GetDbContext("UpdateStadiumCommand_Can_Update_Stadium"))
            {
                var fakeStadiumRepo = new StadiumRepository(dbContext);
                var fakeTeamRepo = new TeamRepository(dbContext);

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
                }); await fakePlayerRepo.AddAsync(new Domain.Entities.Player
                {
                    Name = "ThirdPlayer",
                    Surname = "LastName",
                    Height = 1.98,
                    EmailAddress = "test@email.com"
                });

                var TeamsToAdd = new List<Team>
                {
                    new Team
                    {
                        Name = "TeamOne",
                        Longitude = 1.11,
                        Latitude = 1.43,
                        Players = new List<Player> { await fakePlayerRepo.GetPlayerByIdAsync(1)}
                    },
                    new Team
                    {
                        Name = "TeamTwo",
                        Longitude = 2.11,
                        Latitude = 2.43,
                        Players = new List<Player> { await fakePlayerRepo.GetPlayerByIdAsync(2)}
                    },
                    new Team
                    {
                        Name = "TeamThree",
                        Longitude = 3.11,
                        Latitude = 3.43,
                        Players = new List<Player> { await fakePlayerRepo.GetPlayerByIdAsync(3)}
                    },
                };

                foreach (var TeamToAdd in TeamsToAdd)
                {
                    await fakeTeamRepo.AddAsync(TeamToAdd);
                }

                await fakeStadiumRepo.AddAsync(new Domain.Entities.Stadium
                {
                    Name = "StadiumName",
                    TeamId = 2
                });

                var fakeLogger = new Mock<ILogger<UpdateStadiumCommandHandler>>();
                var handler = new UpdateStadiumCommandHandler(fakeStadiumRepo, GetMapper(), fakeLogger.Object);

                var command = new UpdateStadiumCommand
                {
                    Id = 1,
                    Name = "NewStadiumName",
                    TeamId = 1
                };

                var result = await handler.Handle(command, default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(command.Name, result.StadiumLookupModel.Name);
                Assert.Equal(command.TeamId, result.StadiumLookupModel.Team.Id);
            }
        }
    }
}
