using AutoMapper;
using FootballManager.Application.Teams.Models;
using FootballManager.Persistence.Repositories.TeamRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Teams.Commands
{
    public class UpdateTeamCommand : IRequest<TeamViewModel>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public class UpdateTeamCommandHandler : RequestHandlerBase, IRequestHandler<UpdateTeamCommand, TeamViewModel>
        {
            private readonly IMapper _mapper;
            private readonly ITeamRepository _teamRepository;

            public UpdateTeamCommandHandler(ITeamRepository teamRepository, IMapper mapper, ILogger<UpdateTeamCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _teamRepository = teamRepository;
            }

            public async Task<TeamViewModel> Handle(UpdateTeamCommand updateTeamCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<TeamViewModel>(async response =>
                {
                    var teamToUpdate = await _teamRepository.GetTeamByIdAsync(updateTeamCommand.Id);

                    teamToUpdate.Name = updateTeamCommand.Name;
                    teamToUpdate.Longitude = updateTeamCommand.Longitude;
                    teamToUpdate.Latitude = updateTeamCommand.Latitude;

                    var updatedTeam = await _teamRepository.UpdateAsync(teamToUpdate);

                    var teamDto = _mapper.Map<TeamLookupModel>(updatedTeam);
                    response.TeamLookupModel = teamDto;
                });
            });
        }
    }
}
