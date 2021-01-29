using AutoMapper;
using AutoMapper.QueryableExtensions;
using FootballManager.Application.Stadiums.Models;
using FootballManager.Persistence.Repositories.StadiumRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Stadiums.Queries
{
    public class GetStadiumByIdQuery : IRequest<StadiumViewModel>
    {
        public long Id { get; set; }

        public class GetStadiumByIdQueryHandler : RequestHandlerBase, IRequestHandler<GetStadiumByIdQuery, StadiumViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IStadiumRepository _StadiumRepository;

            public GetStadiumByIdQueryHandler(IStadiumRepository StadiumRepository, IMapper mapper, ILogger<GetStadiumByIdQueryHandler> logger)
                : base(logger)
            {
                _StadiumRepository = StadiumRepository;
                _mapper = mapper;
            }

            public async Task<StadiumViewModel> Handle(GetStadiumByIdQuery request, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return ExecuteAsync<StadiumViewModel>(async response =>
                {
                    var Stadium = await _StadiumRepository.GetStadiumByIdAsync(request.Id);
                    var StadiumDto = _mapper.Map<StadiumLookupModel>(Stadium);
                    response.StadiumLookupModel = StadiumDto;
                });
            });
        }
    }
}
