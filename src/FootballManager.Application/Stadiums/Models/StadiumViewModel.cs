using FootballManager.Application.Models;

namespace FootballManager.Application.Stadiums.Models
{
    public class StadiumViewModel : NotificationViewModel
    {
        public StadiumViewModel()
        {
        }

        public StadiumLookupModel StadiumLookupModel { get; set; }
    }
}