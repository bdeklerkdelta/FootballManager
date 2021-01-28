using AutoMapper;
using FootballManager.Application.Interfaces.Mapping;
using FootballManager.Domain.Entities;
using static FootballManager.Domain.Enumerations.EnumBag;

namespace FootballManager.Application.Players.Models
{
    public class PlayerDeleteModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public DataState DataState { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Player, PlayerDeleteModel>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.DataState, opt => opt.MapFrom(c => c.DataState));
        }
    }
}
