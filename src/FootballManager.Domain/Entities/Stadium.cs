using FootballManager.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Domain.Entities
{
    public class Stadium : EntityBase
    {     
        public string Name { get; set; }

        public Team Team { get; set; }

        public long TeamId { get; set; }
    }
}
