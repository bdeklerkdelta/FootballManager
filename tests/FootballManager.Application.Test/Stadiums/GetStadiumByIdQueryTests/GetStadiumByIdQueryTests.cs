using FootballManager.Application.Stadiums.Commands;
using FootballManager.Application.Stadiums.Queries;
using FootballManager.Domain.Entities;
using FootballManager.Persistence.Repositories.TeamRepository;
using FootballManager.Persistence.Repositories.StadiumRepository;
using FootballManager.Persistence.Repositories.PlayerRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Stadiums.Queries.GetStadiumByIdQuery;

namespace FootballManager.Application.Test.Stadiums.GetStadiumByIdQueryTests
{
    public class GetStadiumByIdQueryTests : BaseHandlerTest
    {
        [Fact]
        public async Task GetStadiumByIdQuery_Can_Return_Stadium()
        {
            var dbContext = GetDbContext("GetStadiumByIdQuery_Can_Return_Stadium");

            using (dbContext)
            {
                var fakeStadiumRepo = new StadiumRepository(dbContext);
                var fakeTeamRepo = new TeamRepository(dbContext);
                var fakeLogger = new Mock<ILogger<GetStadiumByIdQueryHandler>>();
                var handler = new GetStadiumByIdQueryHandler(fakeStadiumRepo, GetMapper(), fakeLogger.Object);

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

                var StadiumsToAdd = new List<Stadium>
                {
                    new Stadium
                    {
                        Name = "StadiumOne",
                        TeamId = 1
                    },
                    new Stadium
                    {
                        Name = "StadiumTwo",
                        TeamId = 2
                    },
                    new Stadium
                    {
                        Name = "StadiumThree",
                        TeamId = 3
                    }
                };

                foreach (var StadiumToAdd in StadiumsToAdd)
                {
                    await fakeStadiumRepo.AddAsync(StadiumToAdd);
                }

                var result = await handler.Handle(new GetStadiumByIdQuery()
                {
                    Id = 2
                }, default);

                Assert.False(result.Notifications.HasErrors());

                var secondStadium = result.StadiumLookupModel;

                Assert.Equal(StadiumsToAdd[1].Name, secondStadium.Name);
                Assert.Equal(StadiumsToAdd[1].Team.Id, secondStadium.Team.Id);
            }
        }
    }
}
