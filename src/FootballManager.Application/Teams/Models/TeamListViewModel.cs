using FootballManager.Application.Models;
using System.Collections.Generic;

namespace FootballManager.Application.Teams.Models
{
    public class TeamListViewModel : NotificationViewModel
    {
        public TeamListViewModel()
        {
        }

        public IList<TeamLookupModel> Teams { get; set; }

    }
}