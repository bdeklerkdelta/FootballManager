using AutoMapper;
using FootballManager.Application.Players.Models;
using FootballManager.Persistence.Repositories.PlayerRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Players.Commands
{
    public class UpdatePlayerCommand : IRequest<PlayerViewModel>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public double Height { get; set; }

        public string EmailAddress { get; set; }

        public class UpdatePlayerCommandHandler : RequestHandlerBase, IRequestHandler<UpdatePlayerCommand, PlayerViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IPlayerRepository _playerRepository;

            public UpdatePlayerCommandHandler(IPlayerRepository playerRepository, IMapper mapper, ILogger<UpdatePlayerCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _playerRepository = playerRepository;
            }

            public async Task<PlayerViewModel> Handle(UpdatePlayerCommand updatePlayerCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<PlayerViewModel>(async response =>
                {
                    var playerToUpdate = await _playerRepository.GetPlayerByIdAsync(updatePlayerCommand.Id);

                    playerToUpdate.Name = updatePlayerCommand.Name;
                    playerToUpdate.Surname = updatePlayerCommand.Surname;
                    playerToUpdate.Height = updatePlayerCommand.Height;
                    playerToUpdate.EmailAddress = updatePlayerCommand.EmailAddress;

                    var addedPlayer = await _playerRepository.UpdateAsync(playerToUpdate);

                    var playerDto = _mapper.Map<PlayerLookupModel>(addedPlayer);
                    response.PlayerLookupModel = playerDto;
                });
            });
        }
    }
}
