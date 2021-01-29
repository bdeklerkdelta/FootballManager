using AutoMapper;
using FootballManager.Application.Interfaces.Mapping;
using FootballManager.Domain.Entities;
using static FootballManager.Domain.Enumerations.EnumBag;

namespace FootballManager.Application.Stadiums.Models
{
    public class StadiumDeleteModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public DataState DataState { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Stadium, StadiumDeleteModel>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.DataState, opt => opt.MapFrom(c => c.DataState));
        }
    }
}
