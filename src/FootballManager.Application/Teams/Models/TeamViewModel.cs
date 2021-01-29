using FootballManager.Application.Models;

namespace FootballManager.Application.Teams.Models
{
    public class TeamViewModel : NotificationViewModel
    {
        public TeamViewModel()
        {
        }

        public TeamLookupModel TeamLookupModel { get; set; }
    }
}