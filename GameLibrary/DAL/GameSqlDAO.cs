using GameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.DAL
{
    public class GameSqlDAO : IGameDAO
    {
        private string ConnectionString { get; }

        public GameSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IList<GameModel> GetGames()
        {
            IList<GameModel> games = new List<GameModel>();
            string sql = "select * from games g join game_category gc on g.id = gc.game_id join category c on c.id = gc.category_id join game_mechanics gm on g.id = gm.game_id join mechanic m on m.id = gm.mechanic_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GameModel game = ConvertReaderToGameModel(reader);
                        if (games.Contains(game))
                        {
                            foreach (string category in game.Categories)
                            {
                                games[games.IndexOf(game)].Categories.Add(category);
                            }
                            foreach (string mechanic in game.Mechanics)
                            {
                                games[games.IndexOf(game)].Mechanics.Add(mechanic);
                            }

                        }
                        else
                        {
                            games.Add(game);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return games;
        }

        private GameModel ConvertReaderToGameModel(SqlDataReader reader)
        {
            GameModel game = new GameModel()
            {
                Id = Convert.ToInt32(reader["id"]),
                Title = Convert.ToString(reader["title"]),
                ImageUrl = Convert.ToString(reader["image"]),
                Description = Convert.ToString(reader["description"]),
                MinPlayers = Convert.ToInt32(reader["min_players"]),
                MaxPlayers = Convert.ToInt32(reader["max_players"]),
                ReccommendedAge = Convert.ToInt32(reader["recommended_age"]),
                AvgPlayTime = Convert.ToInt32(reader["avg_play_time"]),
                BGGRating = Convert.ToDouble(reader["bgg_rating"]),
                BGGWeight = Convert.ToDouble(reader["bgg_weight"]),
                Quantity = Convert.ToInt32(reader["quantity"]),
            };

            game.Categories.Add(Convert.ToString(reader["category_name"]));
            game.Mechanics.Add(Convert.ToString(reader["mechanic_name"]));

            return game;
        }

        public bool AddGame(GameModel game)
        {
            bool gameAdded = false;
            string sql = "insert into games values (@title, @image, @desc, @min, @max, @age, @playTime, @weight, @rating, @quantity); select @@identity as id";

            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@title", game.Title);
                    cmd.Parameters.AddWithValue("@image", game.ImageUrl);
                    cmd.Parameters.AddWithValue("@desc", game.Description);
                    cmd.Parameters.AddWithValue("@min", game.MinPlayers);
                    cmd.Parameters.AddWithValue("@max", game.MaxPlayers);
                    cmd.Parameters.AddWithValue("@age", game.ReccommendedAge);
                    cmd.Parameters.AddWithValue("@playTime", game.AvgPlayTime);
                    cmd.Parameters.AddWithValue("@weight", game.BGGWeight);
                    cmd.Parameters.AddWithValue("@rating", game.BGGRating);
                    cmd.Parameters.AddWithValue("@quantity", game.Quantity);

                    int newGameId = Convert.ToInt16(cmd.ExecuteScalar());

                    IDictionary<string, int> categoryIds = this.GetCategoryDictionary();
                    IDictionary<string, int> mechanicIds = this.GetMechanicDictionary();


                    foreach (string category in game.Categories)
                    {
                        int categoryId = categoryIds[category];
                        cmd = new SqlCommand("insert into games_categories values (@category, @game)", conn);
                        cmd.Parameters.AddWithValue("@category", categoryId);
                        cmd.Parameters.AddWithValue("@game", newGameId);
                        cmd.ExecuteNonQuery();
                    }
                    foreach (string mechanic in game.Mechanics)
                    {
                        int mechanicId = mechanicIds[mechanic];
                        cmd = new SqlCommand("insert into games_mechanics values (@mechanics, @game)", conn);
                        cmd.Parameters.AddWithValue("@mechanics", mechanicId);
                        cmd.Parameters.AddWithValue("@game", newGameId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return gameAdded;
        }

        public IDictionary<string, int> GetCategoryDictionary()
        {
            IDictionary<string, int> categoryIds = new Dictionary<string, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from category", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string key = Convert.ToString(reader["category_name"]);
                        int value = Convert.ToInt32(reader["id"]);
                        categoryIds.Add(key, value);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return categoryIds;
        }

        public IDictionary<string, int> GetMechanicDictionary()
        {
            IDictionary<string, int> mechanicIds = new Dictionary<string, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from mechanic", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string key = Convert.ToString(reader["mechanic_name"]);
                        int value = Convert.ToInt32(reader["id"]);
                        mechanicIds.Add(key, value);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return mechanicIds;
        }


        public GameModel GetGame(int gameId)
        {
            GameModel gameModel = new GameModel();
            string sql = "select * from games g join game_category gc on g.id = gc.game_id join category c on c.id = gc.category_id join game_mechanics gm on g.id = gm.game_id join mechanic m on m.id = gm.mechanic_id where g.id = @gameId";

            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@gameId", gameId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GameModel game = ConvertReaderToGameModel(reader);
                        if (gameModel.Id == game.Id)
                        {
                            foreach (string category in game.Categories)
                            {
                                gameModel.Categories.Add(category);
                            }
                            foreach (string mechanic in game.Mechanics)
                            {
                                gameModel.Mechanics.Add(mechanic);
                            }

                        }
                        else
                        {
                            gameModel = game;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return gameModel;
        }

        public IList<GameModel> GetGamesByPlayerCount(int minPlayers, int maxPlayers, IList<GameModel> allGames)
        {
            var games = from g in allGames where g.MinPlayers >= minPlayers && g.MaxPlayers <= maxPlayers select g;

            return (IList<GameModel>) games;
        }

        public IList<GameModel> GetGamesByPlayTime(int playTime, IList<GameModel> allGames)
        {
            var games = from g in allGames where g.AvgPlayTime <= playTime select g;

            return (IList<GameModel>)games;

        }

        public IList<GameModel> GetGamesByTitle(string title, IList<GameModel> allGames)
        {
            var games = from g in allGames where g.Title.Contains(title) select g;

            return (IList<GameModel>)games;
        }

        public IList<GameModel> GetGameByCategory(string category, IList<GameModel> allGames)
        {
            var games = from g in allGames where g.Categories.Contains(category) select g;

            return (IList<GameModel>)games;
        }

        public IList<GameModel> GetGameByMechanic(string mechanic, IList<GameModel> allGames)
        {
            var games = from g in allGames where g.Mechanics.Contains(mechanic) select g;

            return (IList<GameModel>)games;
        }
    }
}
