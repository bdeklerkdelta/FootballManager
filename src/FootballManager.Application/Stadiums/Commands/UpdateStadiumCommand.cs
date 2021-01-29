using AutoMapper;
using FootballManager.Application.Stadiums.Models;
using FootballManager.Persistence.Repositories.StadiumRepository;
using FootballManager.Persistence.Repositories.TeamRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Stadiums.Commands
{
    public class UpdateStadiumCommand : IRequest<StadiumViewModel>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long TeamId { get; set; }

        public class UpdateStadiumCommandHandler : RequestHandlerBase, IRequestHandler<UpdateStadiumCommand, StadiumViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IStadiumRepository _stadiumRepository;

            public UpdateStadiumCommandHandler(IStadiumRepository stadiumRepository, IMapper mapper, ILogger<UpdateStadiumCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _stadiumRepository = stadiumRepository;
            }

            public async Task<StadiumViewModel> Handle(UpdateStadiumCommand updateStadiumCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<StadiumViewModel>(async response =>
                {
                    var stadiumToUpdate = await _stadiumRepository.GetStadiumByIdAsync(updateStadiumCommand.Id);

                    stadiumToUpdate.Name = updateStadiumCommand.Name;
                    stadiumToUpdate.TeamId = updateStadiumCommand.TeamId;

                    var updatedStadium = await _stadiumRepository.UpdateAsync(stadiumToUpdate);

                    var stadiumDto = _mapper.Map<StadiumLookupModel>(updatedStadium);
                    response.StadiumLookupModel = stadiumDto;
                });
            });
        }
    }
}
