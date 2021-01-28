using AutoMapper;
using AutoMapper.QueryableExtensions;
using FootballManager.Application.Players.Models;
using FootballManager.Persistence.Repositories.PlayerRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Players.Queries
{
    public class GetPlayersQuery : IRequest<PlayerListViewModel>
    {
        public class GetPlayersQueryHandler : RequestHandlerBase, IRequestHandler<GetPlayersQuery, PlayerListViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IPlayerRepository _playerRepository;

            public GetPlayersQueryHandler(IPlayerRepository playerRepository, IMapper mapper, ILogger<GetPlayersQueryHandler> logger)
                : base(logger)
            {
                _playerRepository = playerRepository;
                _mapper = mapper;
            }

            public async Task<PlayerListViewModel> Handle(GetPlayersQuery request, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return Execute<PlayerListViewModel>((response) =>
                {
                    var players = _playerRepository.GetAll();

                    var playersDto = players.AsQueryable().ProjectTo<PlayerLookupModel>(_mapper.ConfigurationProvider).ToList();
                    response.Players = playersDto;
                });
            });
        }
    }
}
