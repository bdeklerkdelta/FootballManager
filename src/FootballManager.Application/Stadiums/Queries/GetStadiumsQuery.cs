using AutoMapper;
using AutoMapper.QueryableExtensions;
using FootballManager.Application.Stadiums.Models;
using FootballManager.Persistence.Repositories.StadiumRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FootballManager.Application.Stadiums.Queries
{
    public class GetStadiumsQuery : IRequest<StadiumListViewModel>
    {
        public class GetStadiumsQueryHandler : RequestHandlerBase, IRequestHandler<GetStadiumsQuery, StadiumListViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IStadiumRepository _StadiumRepository;

            public GetStadiumsQueryHandler(IStadiumRepository StadiumRepository, IMapper mapper, ILogger<GetStadiumsQueryHandler> logger)
                : base(logger)
            {
                _StadiumRepository = StadiumRepository;
                _mapper = mapper;
            }

            public async Task<StadiumListViewModel> Handle(GetStadiumsQuery request, CancellationToken cancellationToken) => await Task.Run(() =>
            {
                return Execute<StadiumListViewModel>((response) =>
                {
                    var stadiums = _StadiumRepository.GetAll();
                    var stadiumsDto = new List<StadiumLookupModel>();

                    foreach(var stadium in stadiums)
                    {
                        stadiumsDto.Add(_mapper.Map<StadiumLookupModel>(stadium));
                    }

                    response.Stadiums = stadiumsDto;
                });
            });
        }
    }
}
