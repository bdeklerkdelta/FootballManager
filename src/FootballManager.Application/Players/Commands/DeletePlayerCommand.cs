using AutoMapper;
using FootballManager.Application.Players.Models;
using FootballManager.Persistence.Repositories.PlayerRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Players.Commands
{
    public class DeletePlayerCommand : IRequest<PlayerDeleteViewModel>
    {
        public long Id { get; set; }

        public class DeletePlayerCommandHandler : RequestHandlerBase, IRequestHandler<DeletePlayerCommand, PlayerDeleteViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IPlayerRepository _playerRepository;

            public DeletePlayerCommandHandler(IPlayerRepository playerRepository, IMapper mapper, ILogger<DeletePlayerCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _playerRepository = playerRepository;
            }

            public async Task<PlayerDeleteViewModel> Handle(DeletePlayerCommand updatePlayerCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<PlayerDeleteViewModel>(async response =>
                {
                    var playerToDelete = await _playerRepository.GetPlayerByIdAsync(updatePlayerCommand.Id);

                    playerToDelete.DataState = Domain.Enumerations.EnumBag.DataState.Inactive;

                    var deletedPlayer = await _playerRepository.UpdateAsync(playerToDelete);

                    var playerDto = _mapper.Map<PlayerDeleteModel>(deletedPlayer);
                    response.PlayerDeleteModel = playerDto;
                });
            });
        }
    }
}
