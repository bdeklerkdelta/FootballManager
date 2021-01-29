using AutoMapper;
using FootballManager.Application.Teams.Models;
using FootballManager.Domain.Entities;
using FootballManager.Persistence.Repositories.PlayerRepository;
using FootballManager.Persistence.Repositories.TeamRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Teams.Commands
{
    public class UpdateTeamPlayersCommand : IRequest<TeamViewModel>
    {
        public long Id { get; set; }

        public ICollection<int> PlayerIds { get; set; }

        public class UpdateTeamPlayersCommandHandler : RequestHandlerBase, IRequestHandler<UpdateTeamPlayersCommand, TeamViewModel>
        {
            private readonly IMapper _mapper;
            private readonly ITeamRepository _teamRepository;
            private readonly IPlayerRepository _playerRepository;

            public UpdateTeamPlayersCommandHandler(ITeamRepository teamRepository, IPlayerRepository playerRepository, IMapper mapper, ILogger<UpdateTeamPlayersCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _teamRepository = teamRepository;
                _playerRepository = playerRepository;
            }

            public async Task<TeamViewModel> Handle(UpdateTeamPlayersCommand updateTeamPlayersCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<TeamViewModel>(async response =>
                {
                    var teamToUpdate = await _teamRepository.GetTeamByIdAsync(updateTeamPlayersCommand.Id);

                    var playersToAdd = new List<Player>();

                    foreach (var playerId in updateTeamPlayersCommand.PlayerIds)
                    {
                        playersToAdd.Add(await _playerRepository.GetPlayerByIdAsync(playerId));
                    }

                    foreach(var playerToAdd in playersToAdd)
                    {
                        playerToAdd.Team = teamToUpdate;
                    }

                    var updatedTeam = await _teamRepository.UpdateAsync(teamToUpdate);

                    var teamDto = _mapper.Map<TeamLookupModel>(updatedTeam);
                    response.TeamLookupModel = teamDto;
                });
            });
        }
    }
}
