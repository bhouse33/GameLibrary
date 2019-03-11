using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int ReccommendedAge { get; set; }
        public int AvgPlayTime { get; set; }
        public double BGGWeight { get; set; }
        public double BGGRating { get; set; }
        public bool IsBorrowed { get; set; }
        public IList<string> Genres { get; set; }
        public int Quantity { get; set; }

        public override bool Equals(object obj)
        {
            var game = obj as GameModel;

            if (game == null)
            {
                return false;
            }

            return this.Id == game.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
