using FootballManager.Application.Models;
using System.Collections.Generic;

namespace FootballManager.Application.Players.Models
{
    public class PlayerListViewModel : NotificationViewModel
    {
        public PlayerListViewModel()
        {
            Players = new List<PlayerLookupModel>();
        }

        public IList<PlayerLookupModel> Players { get; set; }
    }
}
