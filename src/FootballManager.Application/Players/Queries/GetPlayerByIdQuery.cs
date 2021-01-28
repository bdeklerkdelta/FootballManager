using AutoMapper;
using FootballManager.Application.Players.Models;
using FootballManager.Persistence.Repositories.PlayerRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Players.Queries
{
    public class GetPlayerByIdQuery : IRequest<PlayerViewModel>
    {
        public long Id { get; set; }

        public class GetPlayerByIdQueryHandler : RequestHandlerBase, IRequestHandler<GetPlayerByIdQuery, PlayerViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IPlayerRepository _playerRepository;

            public GetPlayerByIdQueryHandler(IPlayerRepository playerRepository, IMapper mapper, ILogger<GetPlayerByIdQueryHandler> logger)
                : base(logger)
            {
                _playerRepository = playerRepository;
                _mapper = mapper;
            }

            public async Task<PlayerViewModel> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<PlayerViewModel>(async response =>
                {
                    var player = await _playerRepository.GetPlayerByIdAsync(request.Id);

                    var playerDto = _mapper.Map<PlayerLookupModel>(player);
                    response.PlayerLookupModel = playerDto;
                });
            });
        }
    }
}
