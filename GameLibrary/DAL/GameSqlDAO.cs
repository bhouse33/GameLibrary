using GameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.DAL
{
    public class GameSqlDAO
    {
        private string ConnectionString { get; }

        public GameSqlDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public IList<GameModel> GetGames()
        {
            IList<GameModel> games = new List<GameModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from games g join game_genre gg on g.id = gg.game_id join genre on genre.genre_id = gg.id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GameModel game = ConvertReaderToGameModel(reader);
                        if (games.Contains(game))
                        {
                            foreach (string genre in game.Genres)
                            {
                                games[games.IndexOf(game)].Genres.Add(genre);
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

            game.Genres.Add(Convert.ToString(reader["name"]));


            return game;
        }

        public bool AddGame(GameModel game)
        {
            bool gameAdded = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("insert into games values (@title, @image, @desc, @min, @max, @age, @playTime, @weight, @rating, @quantity); select @@identity as id", conn);
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

                    Dictionary<string, int> genreIds = this.GetGenreDictionary();

                    foreach (string genre in game.Genres)
                    {
                        int genreId = genreIds[genre];
                        cmd = new SqlCommand("insert into games_genres values (@genre, @game)", conn);
                        cmd.Parameters.AddWithValue("@genre", genreId);
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

        public Dictionary<string,int> GetGenreDictionary()
        {
            Dictionary<string, int> genreIds = new Dictionary<string, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.ConnectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from genre", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string key = Convert.ToString(reader["name"]);
                        int value = Convert.ToInt32(reader["id"]);
                        genreIds.Add(key, value);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return genreIds;
        }
    }
}
