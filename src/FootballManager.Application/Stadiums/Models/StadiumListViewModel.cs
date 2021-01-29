using FootballManager.Application.Models;
using System.Collections.Generic;

namespace FootballManager.Application.Stadiums.Models
{
    public class StadiumListViewModel : NotificationViewModel
    {
        public StadiumListViewModel()
        {
        }

        public IList<StadiumLookupModel> Stadiums { get; set; }

    }
}