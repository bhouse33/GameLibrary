using GameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.DAL
{
    public interface IGameDAO
    {
        IList<GameModel> GetGames();
        bool AddGame(GameModel game);
        IDictionary<string, int> GetGenreDictionary();
        GameModel GetGame(int gameId);
    }
}
