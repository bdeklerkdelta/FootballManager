using FootballManager.Application.Models;

namespace FootballManager.Application.Players.Models
{
    public class PlayerViewModel : NotificationViewModel
    {
        public PlayerViewModel()
        {
        }

        public PlayerLookupModel PlayerLookupModel { get; set; }
    }
}
