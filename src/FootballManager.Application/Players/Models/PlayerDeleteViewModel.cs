using FootballManager.Application.Models;
namespace FootballManager.Application.Players.Models
{
    public class PlayerDeleteViewModel : NotificationViewModel
    {
        public PlayerDeleteViewModel()
        {
        }

        public PlayerDeleteModel PlayerDeleteModel { get; set; }
    }
}
