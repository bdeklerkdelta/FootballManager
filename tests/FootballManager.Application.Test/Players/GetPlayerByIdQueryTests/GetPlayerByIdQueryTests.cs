using FootballManager.Application.Players.Commands;
using FootballManager.Application.Players.Queries;
using FootballManager.Domain.Entities;
using FootballManager.Persistence.Repositories.PlayerRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Players.Queries.GetPlayerByIdQuery;

namespace FootballManager.Application.Test.Players.GetPlayerByIdQueryTests
{
    public class GetPlayerByIdQueryTests : BaseHandlerTest
    {
        [Fact]
        public async Task GetPlayerByIdQuery_Can_Return_Player()
        {
            var dbContext = GetDbContext("GetPlayerByIdQuery_Can_Return_Player");

            using (dbContext)
            {
                var fakeRepo = new PlayerRepository(dbContext);
                var fakeLogger = new Mock<ILogger<GetPlayerByIdQueryHandler>>();
                var handler = new GetPlayerByIdQueryHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var playersToAdd = new List<Player>
                {
                    new Player
                    {
                        Name = "FirstName",
                        Surname = "LastName",
                        Height = 1.98,
                        EmailAddress = "test@email.com"
                    },
                    new Player
                    {
                        Name = "SecondName",
                        Surname = "SecondLastName",
                        Height = 1.98,
                        EmailAddress = "test@email.com"
                    },
                    new Player
                    {
                        Name = "ThirdName",
                        Surname = "ThirdLastName",
                        Height = 1.98,
                        EmailAddress = "test@email.com"
                    },
                };

                foreach(var playerToAdd in playersToAdd)
                {
                    await fakeRepo.AddAsync(playerToAdd);
                }

                var result = await handler.Handle(new GetPlayerByIdQuery()
                {
                    Id = 2
                }, default);

                Assert.False(result.Notifications.HasErrors());

                var secondPlayer = result.PlayerLookupModel;

                Assert.Equal(playersToAdd[1].Name, secondPlayer.Name);
                Assert.Equal(playersToAdd[1].Surname, secondPlayer.Surname);
                Assert.Equal(playersToAdd[1].Height, secondPlayer.Height);
                Assert.Equal(playersToAdd[1].EmailAddress, secondPlayer.EmailAddress);
            }
        }
    }
}
