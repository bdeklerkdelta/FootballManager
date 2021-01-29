using AutoMapper;
using FootballManager.Application.Models;
using FootballManager.Application.Teams.Models;
using FootballManager.Domain.Entities;
using FootballManager.Domain.ValueObjects;
using FootballManager.Persistence.Repositories.PlayerRepository;
using FootballManager.Persistence.Repositories.TeamRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Teams.Commands
{
    public class AddTeamCommand : IRequest<TeamViewModel>
    {
        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public ICollection<int> PlayerIds { get; set; }

        public class AddTeamCommandHandler : RequestHandlerBase, IRequestHandler<AddTeamCommand, TeamViewModel>
        {
            private readonly IMapper _mapper;
            private readonly ITeamRepository _teamRepository;
            private readonly IPlayerRepository _playerRepository;

            public AddTeamCommandHandler(ITeamRepository teamRepository, IPlayerRepository playerRepository, IMapper mapper, ILogger<AddTeamCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _teamRepository = teamRepository;
                _playerRepository = playerRepository;
            }

            public async Task<TeamViewModel> Handle(AddTeamCommand addTeamCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<TeamViewModel>(async response =>
                {
                    var playersToAdd = new List<Player>();

                    var teamToAdd = new Team
                    {
                        Name = addTeamCommand.Name,
                        Longitude = addTeamCommand.Longitude,
                        Latitude = addTeamCommand.Latitude,
                        Players = playersToAdd
                    };

                    var addedTeam = await _teamRepository.AddAsync(teamToAdd);

                    foreach (var playerId in addTeamCommand.PlayerIds)
                    {
                        var player = await _playerRepository.GetPlayerByIdAsync(playerId);

                        player.Team = addedTeam;
                        await _playerRepository.UpdateAsync(player);
                    }

                    var updatedTeam = await _teamRepository.UpdateAsync(addedTeam);

                    var teamDto = _mapper.Map<TeamLookupModel>(addedTeam);
                    response.TeamLookupModel = teamDto;
                });
            });
        }
    }
}
