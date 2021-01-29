using AutoMapper;
using FootballManager.Application.Models;
using FootballManager.Application.Stadiums.Models;
using FootballManager.Domain.Entities;
using FootballManager.Domain.ValueObjects;
using FootballManager.Persistence.Repositories.TeamRepository;
using FootballManager.Persistence.Repositories.StadiumRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Stadiums.Commands
{
    public class AddStadiumCommand : IRequest<StadiumViewModel>
    {
        public string Name { get; set; }

        public long TeamId { get; set; }

        public class AddStadiumCommandHandler : RequestHandlerBase, IRequestHandler<AddStadiumCommand, StadiumViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IStadiumRepository _stadiumRepository;
            private readonly ITeamRepository _teamRepository;

            public AddStadiumCommandHandler(IStadiumRepository stadiumRepository, ITeamRepository teamRepository, IMapper mapper, ILogger<AddStadiumCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _stadiumRepository = stadiumRepository;
                _teamRepository = teamRepository;
            }

            public async Task<StadiumViewModel> Handle(AddStadiumCommand addStadiumCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<StadiumViewModel>(async response =>
                {
                    var team = await _teamRepository.GetTeamByIdAsync(addStadiumCommand.TeamId);

                    var StadiumToAdd = new Stadium
                    {
                        Name = addStadiumCommand.Name,
                        TeamId = addStadiumCommand.TeamId
                    };

                    var addedStadium = await _stadiumRepository.AddAsync(StadiumToAdd);

                    var StadiumDto = _mapper.Map<StadiumLookupModel>(addedStadium);
                    response.StadiumLookupModel = StadiumDto;
                });
            });
        }
    }
}
