using AutoMapper;
using FootballManager.Application.Teams.Models;
using FootballManager.Persistence.Repositories.TeamRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Teams.Commands
{
    public class DeleteTeamCommand : IRequest<TeamDeleteViewModel>
    {
        public long Id { get; set; }

        public class DeleteTeamCommandHandler : RequestHandlerBase, IRequestHandler<DeleteTeamCommand, TeamDeleteViewModel>
        {
            private readonly IMapper _mapper;
            private readonly ITeamRepository _TeamRepository;

            public DeleteTeamCommandHandler(ITeamRepository TeamRepository, IMapper mapper, ILogger<DeleteTeamCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _TeamRepository = TeamRepository;
            }

            public async Task<TeamDeleteViewModel> Handle(DeleteTeamCommand deleteTeamCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<TeamDeleteViewModel>(async response =>
                {
                    var teamToDelete = await _TeamRepository.GetTeamByIdAsync(deleteTeamCommand.Id);

                    teamToDelete.DataState = Domain.Enumerations.EnumBag.DataState.Inactive;

                    var deletedTeam = await _TeamRepository.UpdateAsync(teamToDelete);

                    var teamDto = _mapper.Map<TeamDeleteModel>(deletedTeam);
                    response.TeamDeleteModel = teamDto;
                });
            });
        }
    }
}
