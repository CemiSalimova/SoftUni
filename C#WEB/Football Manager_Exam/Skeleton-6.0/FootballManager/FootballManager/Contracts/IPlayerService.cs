using FootballManager.ViewModels.PlayersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Contracts
{
    public interface IPlayerService
    {
        IEnumerable<PlayersCollectionViewModel> GetAllPlayers();
        (bool created, string error) AddPlayer(PlayerViewModel model);
        void AddToCollection(string tripId, string id);
        IEnumerable<PlayersCollectionViewModel> GetCollectedPlayers(string userId);
        void RemoveFromCollection(string userId, string playerId);
    }
}
