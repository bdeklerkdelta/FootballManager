using FootballManager.Application.Models;
namespace FootballManager.Application.Teams.Models
{
    public class TeamDeleteViewModel : NotificationViewModel
    {
        public TeamDeleteViewModel()
        {
        }

        public TeamDeleteModel TeamDeleteModel { get; set; }
    }
}
