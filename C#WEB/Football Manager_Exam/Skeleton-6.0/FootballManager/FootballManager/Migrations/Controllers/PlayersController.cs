using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using FootballManager.Contracts;
using FootballManager.ViewModels.PlayersViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller

    {
        private readonly IPlayerService playerService;
        private readonly IValidationService validationService;
        public PlayersController(
            Request request,
            IPlayerService _playerService,
            IValidationService _validationService)
            : base(request)
        {
            playerService = _playerService;
            validationService = _validationService;
        }
        [Authorize]
        public Response All()
        {
            IEnumerable<PlayersCollectionViewModel> players = playerService.GetAllPlayers();

            return View(players);
        }


        [Authorize]
        public Response Add() => View();

        [HttpPost]
        [Authorize]
        public Response Add(PlayerViewModel model)
        {
            try
            {
                var (created, error) = playerService.AddPlayer(model);

                if (!created)
                {
                    return View(new { ErrorMessage = error }, "/Error");
                }


                return Redirect("/Players/All");
            }
            catch (Exception)
            {

                return Redirect("/");
            }
         
        }
        [Authorize]
        public Response Collection()
        {
            try
            {
                IEnumerable<PlayersCollectionViewModel> players = playerService.GetCollectedPlayers(User.Id);
                return View(new { IsAuthenticated = true, players });
            }
            catch (Exception)
            {

                return Redirect("/");
            }
           


        }


       
        public Response AddToCollection(string playerId)
        {
            try
            {
                playerService.AddToCollection(User.Id, playerId);
                return Redirect("/Players/Collection");
            }
            catch (Exception)
            {

                return Redirect("/Players/All");
            }
            


            
        }
        public Response RemoveFromCollection(string playerId)
        {

            playerService.RemoveFromCollection(User.Id, playerId);


            return Redirect("/Players/Collection");
        }

    }
}
