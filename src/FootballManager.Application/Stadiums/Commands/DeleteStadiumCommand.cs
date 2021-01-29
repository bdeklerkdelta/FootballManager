using AutoMapper;
using FootballManager.Application.Stadiums.Models;
using FootballManager.Persistence.Repositories.StadiumRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Stadiums.Commands
{
    public class DeleteStadiumCommand : IRequest<StadiumDeleteViewModel>
    {
        public long Id { get; set; }

        public class DeleteStadiumCommandHandler : RequestHandlerBase, IRequestHandler<DeleteStadiumCommand, StadiumDeleteViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IStadiumRepository _stadiumRepository;

            public DeleteStadiumCommandHandler(IStadiumRepository stadiumRepository, IMapper mapper, ILogger<DeleteStadiumCommandHandler> logger)
                : base(logger)
            {
                _mapper = mapper;
                _stadiumRepository = stadiumRepository;
            }

            public async Task<StadiumDeleteViewModel> Handle(DeleteStadiumCommand deleteStadiumCommand, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<StadiumDeleteViewModel>(async response =>
                {
                    var stadiumToDelete = await _stadiumRepository.GetStadiumByIdAsync(deleteStadiumCommand.Id);

                    stadiumToDelete.DataState = Domain.Enumerations.EnumBag.DataState.Inactive;

                    var deletedStadium = await _stadiumRepository.UpdateAsync(stadiumToDelete);

                    var stadiumDto = _mapper.Map<StadiumDeleteModel>(deletedStadium);
                    response.StadiumDeleteModel = stadiumDto;
                });
            });
        }
    }
}
