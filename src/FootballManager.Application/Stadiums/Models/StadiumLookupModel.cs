using AutoMapper;
using FootballManager.Application.Interfaces.Mapping;
using FootballManager.Application.Teams.Models;
using FootballManager.Domain.Entities;
using FootballManager.Domain.ValueObjects;
using System.Collections.Generic;

namespace FootballManager.Application.Stadiums.Models
{
    public class StadiumLookupModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public TeamLookupModel Team { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Stadium, StadiumLookupModel>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dto => dto.Team, opt => opt.MapFrom(c => c.Team));
        }
    }
}
