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
        IDictionary<string, int> GetCategoryDictionary();
        IDictionary<string, int> GetMechanicDictionary();
        GameModel GetGame(int gameId);
        IList<GameModel> GetGamesByPlayerCount(int minPlayers, int maxPlayers, IList<GameModel> allGames);
        IList<GameModel> GetGamesByPlayTime(int playTime, IList<GameModel> allGames);
        IList<GameModel> GetGamesByTitle(string title, IList<GameModel> allGames);
        IList<GameModel> GetGameByCategory(string category, IList<GameModel> allGames);
        IList<GameModel> GetGameByMechanic(string mechanic, IList<GameModel> allGames);
    }
}
