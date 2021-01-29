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
    public class GetTeamByIdQuery : IRequest<TeamViewModel>
    {
        public long Id { get; set; }

        public class GetTeamByIdQueryHandler : RequestHandlerBase, IRequestHandler<GetTeamByIdQuery, TeamViewModel>
        {
            private readonly IMapper _mapper;
            private readonly ITeamRepository _teamRepository;

            public GetTeamByIdQueryHandler(ITeamRepository teamRepository, IMapper mapper, ILogger<GetTeamByIdQueryHandler> logger)
                : base(logger)
            {
                _teamRepository = teamRepository;
                _mapper = mapper;
            }

            public async Task<TeamViewModel> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return Execute<TeamViewModel>((response) =>
                {
                    // ineffecient. automapper not returning related entity data if only queried on a single entity for some reason
                    var teams = _teamRepository.GetAll();
                    var team = teams.AsQueryable().ProjectTo<TeamLookupModel>(_mapper.ConfigurationProvider).ToList().FirstOrDefault(x => x.Id == request.Id);
                    var teamDto = _mapper.Map<TeamLookupModel>(team);
                    response.TeamLookupModel = teamDto;
                });
            });
        }
    }
}
