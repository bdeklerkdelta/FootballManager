using AutoMapper;
using FootballManager.Application.Interfaces.Mapping;
using FootballManager.Domain.Entities;

namespace FootballManager.Application.Players.Models
{
    public class PlayerLookupModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public double Height { get; set; }

        public string EmailAddress { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Player, PlayerLookupModel>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dto => dto.Surname, opt => opt.MapFrom(c => c.Surname))
                .ForMember(dto => dto.EmailAddress, opt => opt.MapFrom(c => c.EmailAddress.Value));
        }
    }
}
