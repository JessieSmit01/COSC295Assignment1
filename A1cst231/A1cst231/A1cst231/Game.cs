using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace A1cst231
{
    /// <summary>
    /// This class will represent a game object
    /// </summary>
    public class Game
    {
        [PrimaryKey, AutoIncrement] //tell SQLite that this will be the primary key and it will autoincrement
        public int ID { get; set; } //primary key

        public string GameName { get; set; } //name of the game
        public string Description { get; set; } //description
        public double rating; //game rating

        public double Rating
        {
            get { return this.rating; }
            set //on set, check if the rating in within range
            {
                if (value <= 10) //if within range, set it.
                {
                    this.rating = value;
                }
            }
        }

    }
}
