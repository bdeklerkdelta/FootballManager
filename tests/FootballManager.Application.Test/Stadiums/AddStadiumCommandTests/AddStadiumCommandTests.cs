using FootballManager.Application.Stadiums.Commands;
using FootballManager.Persistence.Repositories.PlayerRepository;
using FootballManager.Persistence.Repositories.StadiumRepository;
using FootballManager.Persistence.Repositories.TeamRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Stadiums.Commands.AddStadiumCommand;

namespace FootballManager.Application.Test.Stadiums.AddStadiumCommandTests
{
    public class AddStadiumCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task AddStadiumCommand_Can_Add_Stadium()
        {
            using (var dbContext = GetDbContext("AddStadiumCommand_Can_Add_Stadium"))
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

                var players = fakePlayerRepo.GetAll();
                await fakeTeamRepo.AddAsync(new Domain.Entities.Team
                {
                    Name = "FirstTeam",
                    Players = players.ToList()
                });
            }

            using (var dbContext = GetDbContext("AddStadiumCommand_Can_Add_Stadium"))
            {
                var fakeStadiumRepo = new StadiumRepository(dbContext);
                var fakeTeamRepo = new TeamRepository(dbContext);
                var fakeLogger = new Mock<ILogger<AddStadiumCommandHandler>>();
                var handler = new AddStadiumCommandHandler(fakeStadiumRepo, fakeTeamRepo, GetMapper(), fakeLogger.Object);

                var command = new AddStadiumCommand
                {
                    Name = "TestStadiumName",
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
