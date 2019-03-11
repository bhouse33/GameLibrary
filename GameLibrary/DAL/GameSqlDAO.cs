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
                Title = Convert.ToString(reader["title"]),
                ImageUrl = Convert.ToString(reader["image"]),
                Description = Convert.ToString(reader["description"]),
                MinPlayers = Convert.ToInt32(reader["min_players"]),
                MaxPlayers = Convert.ToInt32(reader["max_players"]),
                ReccommendedAge = Convert.ToInt32(reader["recommended_age"]),
                AvgPlayTime = Convert.ToInt32(reader["avg_play_time"]),
                BGGRating = Convert.ToDouble(reader["bgg_rating"]),
                BGGWeight = Convert.ToDouble(reader["bgg_weight"])
            };

            return game;
        }
    }
}
