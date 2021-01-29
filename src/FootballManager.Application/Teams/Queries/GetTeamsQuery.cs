using AutoMapper;
using AutoMapper.QueryableExtensions;
using FootballManager.Application.Teams.Models;
using FootballManager.Persistence.Repositories.TeamRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Teams.Queries
{
    public class GetTeamsQuery : IRequest<TeamListViewModel>
    {
        public class GetTeamsQueryHandler : RequestHandlerBase, IRequestHandler<GetTeamsQuery, TeamListViewModel>
        {
            private readonly IMapper _mapper;
            private readonly ITeamRepository _teamRepository;

            public GetTeamsQueryHandler(ITeamRepository teamRepository, IMapper mapper, ILogger<GetTeamsQueryHandler> logger)
                : base(logger)
            {
                _teamRepository = teamRepository;
                _mapper = mapper;
            }

            public async Task<TeamListViewModel> Handle(GetTeamsQuery request, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return Execute<TeamListViewModel>((response) =>
                {
                    var teams = _teamRepository.GetAll();
                    var teamsDto = teams.AsQueryable().ProjectTo<TeamLookupModel>(_mapper.ConfigurationProvider).ToList();
                    response.Teams = teamsDto;
                });
            });
        }
    }
}
