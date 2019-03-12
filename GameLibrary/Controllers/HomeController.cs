using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameLibrary.Models;
using GameLibrary.DAL;

namespace GameLibrary.Controllers
{
    public class HomeController : Controller
    {
        private IGameDAO gameDao;

        public HomeController (IGameDAO gameDao)
        {
            this.gameDao = gameDao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Games()
        {
            IList<GameModel> games = gameDao.GetGames();
            return View(games);
        }

        public IActionResult Detail(int id)
        {
            GameModel game = gameDao.GetGame(id);
            return View(game);
        }

        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGame(GameModel game)
        {
            return RedirectToAction("Detail");
        }


    }
}
