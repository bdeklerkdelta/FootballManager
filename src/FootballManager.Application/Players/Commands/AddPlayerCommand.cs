using AutoMapper;
using FootballManager.Application.Models;
using FootballManager.Application.Players.Models;
using FootballManager.Domain.Entities;
using FootballManager.Domain.ValueObjects;
using FootballManager.Persistence.Repositories.PlayerRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Players.Commands
{
    public class AddPlayerCommand : IRequest<PlayerViewModel>
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public double Height { get; set; }

        public string EmailAddress { get; set; }

        public class AddPlayerCommandHandler : RequestHandlerBase, IRequestHandler<AddPlayerCommand, PlayerViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IPlayerRepository _playerRepository;

            public AddPlayerCommandHandler(IPlayerRepository playerRepository, IMapper mapper, ILogger<AddPlayerCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _playerRepository = playerRepository;
            }

            public async Task<PlayerViewModel> Handle(AddPlayerCommand addPlayerCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<PlayerViewModel>(async response =>
                {
                    var playerToAdd = new Player
                    {
                        Name = addPlayerCommand.Name,
                        Surname = addPlayerCommand.Surname,
                        Height = addPlayerCommand.Height,
                        EmailAddress = addPlayerCommand.EmailAddress
                    };

                    var addedPlayer = await _playerRepository.AddAsync(playerToAdd);

                    var playerDto = _mapper.Map<PlayerLookupModel>(addedPlayer);
                    response.PlayerLookupModel = playerDto;
                });
            });
        }
    }
}
