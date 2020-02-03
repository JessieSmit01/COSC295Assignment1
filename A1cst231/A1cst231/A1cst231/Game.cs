using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace A1cst231
{
    public class Game
    {
        [PrimaryKey, AutoIncrement] //tell SQLite that this will be the primary key and it will autoincrement
        public int ID { get; set; } //primary key

        public string GameName { get; set; }
        public string Description { get; set; }

        public double Rating
        {
            get { return this.Rating; }
            set
            {
                if (value <= 10)
                {
                    this.Rating = value;
                }
            }
        }

    }
}
