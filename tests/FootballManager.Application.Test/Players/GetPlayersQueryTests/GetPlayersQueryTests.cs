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
using static FootballManager.Application.Players.Queries.GetPlayersQuery;

namespace FootballManager.Application.Test.Players.GetPlayersQueryTests
{
    public class GetPlayersQueryTests : BaseHandlerTest
    {
        [Fact]
        public async Task GetPlayersQuery_Can_Return_Players()
        {
            var dbContext = GetDbContext("GetPlayersQuery_Can_Return_Players");

            using (dbContext)
            {
                var fakeRepo = new PlayerRepository(dbContext);
                var fakeLogger = new Mock<ILogger<GetPlayersQueryHandler>>();
                var handler = new GetPlayersQueryHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var playerToAdd = new Player
                {
                    Name = "FirstName",
                    Surname = "LastName",
                    Height = 1.98,
                    EmailAddress = "test@email.com"
                };

                await fakeRepo.AddAsync(playerToAdd);

                var result = await handler.Handle(new GetPlayersQuery(), default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(1, result.Players.Count);
                Assert.Equal(playerToAdd.Name, result.Players[0].Name);
                Assert.Equal(playerToAdd.Surname, result.Players[0].Surname);
                Assert.Equal(playerToAdd.Height, result.Players[0].Height);
                Assert.Equal(playerToAdd.EmailAddress, result.Players[0].EmailAddress);
            }
        }
    }
}
