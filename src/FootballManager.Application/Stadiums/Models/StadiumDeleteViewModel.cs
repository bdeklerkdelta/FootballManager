using FootballManager.Application.Models;
namespace FootballManager.Application.Stadiums.Models
{
    public class StadiumDeleteViewModel : NotificationViewModel
    {
        public StadiumDeleteViewModel()
        {
        }

        public StadiumDeleteModel StadiumDeleteModel { get; set; }
    }
}
