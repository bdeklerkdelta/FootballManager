using FootballManager.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Domain.Entities
{
    public class Team : EntityBase
    {     
        public string Name { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
