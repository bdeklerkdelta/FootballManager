using AutoMapper;
using FootballManager.Application.Interfaces.Mapping;
using FootballManager.Application.Players.Models;
using FootballManager.Domain.Entities;
using FootballManager.Domain.ValueObjects;
using System.Collections.Generic;

namespace FootballManager.Application.Teams.Models
{
    public class TeamLookupModel : IHaveCustomMapping
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public ICollection<PlayerLookupModel> Players { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Team, TeamLookupModel>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(dto => dto.Players, opt => opt.MapFrom(c => c.Players));
        }
    }
}
