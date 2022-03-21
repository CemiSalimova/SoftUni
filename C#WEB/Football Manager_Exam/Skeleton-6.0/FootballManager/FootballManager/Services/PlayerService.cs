using FootballManager.Contracts;
using FootballManager.Data.Common;
using FootballManager.Data.Models;
using FootballManager.ViewModels.PlayersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Services
{
    public class PlayerService : IPlayerService

    {
        private readonly IRepository repo;
        private readonly IValidationService validationService;

        public PlayerService(IRepository _repo,
            IValidationService _validationService)
        {
            repo = _repo;
            validationService = _validationService;
        }
        public IEnumerable<PlayersCollectionViewModel> GetAllPlayers()
        {
            return repo.All<Player>()
               .Select(t => new PlayersCollectionViewModel()
               {
                   FullName = t.FullName,
                   Description = t.Description,
                   ImageUrl=t.ImageUrl,
                   Position=t.Position,
                   Speed=t.Speed,
                   Endurance=t.Endurance,
                   Id=t.Id
               });
        }
        public (bool created, string error) AddPlayer(PlayerViewModel model)
        {
            bool created = false;
            string error = null;

            var (isValid, validationError) = validationService.ValidateModel(model);

            if (!isValid)
            {
                return (isValid, validationError);
            }
            Player player = new Player()
            {
                Description = model.Description,
                FullName = model.FullName,
                Speed = model.Speed,
                Endurance = model.Endurance,
                ImageUrl = model.ImageUrl,
                Position = model.Position
            };

                repo.Add(player);
                repo.SaveChanges();
            return (created, error);
        }

        public void AddToCollection(string userId, string playerId)
        { 
            var user = repo.All<User>()
                .FirstOrDefault(u => u.Id == userId);
            var player = repo.All<Player>()
                .FirstOrDefault(t => t.Id == playerId);

            if (user == null || player == null)
            {
                throw new ArgumentException("User or player not found");
            }

            user.UserPlayers.Add(new UserPlayer()
            {
                PlayerId = playerId,
                Player = player,
                User = user,
                UserId = userId
            });

            repo.SaveChanges();
        }
        public IEnumerable<PlayersCollectionViewModel> GetCollectedPlayers(string userId)
        {
            return repo.All<Player>()
                .Where(u=>u.UserPlayers.Any(a=>a.UserId==userId))
               .Select(t => new PlayersCollectionViewModel()
               {
                   FullName = t.FullName,
                   Description = t.Description,
                   ImageUrl = t.ImageUrl,
                   Position = t.Position,
                   Speed = t.Speed,
                   Endurance = t.Endurance,
                   Id = t.Id
               });
        }
        public void RemoveFromCollection(string userId, string playerId)
        {
            var user = repo.All<User>()
                .Where(u => u.Id == userId)
                .FirstOrDefault();
            var player = repo.All<Player>()
                .FirstOrDefault(t => t.Id == playerId);
            var removel = repo.All<UserPlayer>()
                   .Where(u => u.UserId == userId)
                   .Where(u => u.PlayerId == playerId)
                   .ToList();
            removel.Clear();

            repo.SaveChanges();
        }
    }
}
