using FootballManager.Application.Stadiums.Commands;
using FootballManager.Persistence.Repositories.StadiumRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using static FootballManager.Application.Stadiums.Commands.DeleteStadiumCommand;

namespace FootballManager.Application.Test.Stadiums.AddStadiumCommandTests
{
    public class DeleteStadiumCommandTests : BaseHandlerTest
    {
        [Fact]
        public async Task DeleteStadiumCommand_Can_Delete_Stadium()
        {
            using (var dbContext = GetDbContext("DeleteStadiumCommand_Can_Delete_Stadium"))
            {
                var fakeRepo = new StadiumRepository(dbContext);
                await fakeRepo.AddAsync(new Domain.Entities.Stadium
                {
                    Name = "TestStadiumName"
                });
            }

            using (var dbContext = GetDbContext("DeleteStadiumCommand_Can_Delete_Stadium"))
            {
                var fakeRepo = new StadiumRepository(dbContext);
                var fakeLogger = new Mock<ILogger<DeleteStadiumCommandHandler>>();
                var handler = new DeleteStadiumCommandHandler(fakeRepo, GetMapper(), fakeLogger.Object);

                var command = new DeleteStadiumCommand
                {
                    Id = 1,
                };

                var result = await handler.Handle(command, default);

                Assert.False(result.Notifications.HasErrors());

                Assert.Equal(command.Id, result.StadiumDeleteModel.Id);
                Assert.Equal(Domain.Enumerations.EnumBag.DataState.Inactive, result.StadiumDeleteModel.DataState);
            }
        }
    }
}
