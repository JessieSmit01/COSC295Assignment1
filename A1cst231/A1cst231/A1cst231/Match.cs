﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace A1cst231
{
    public class Match
    {
        [PrimaryKey, AutoIncrement] //tell SQLite that this will be the primary key and it will autoincrement
        public int ID { get; set; } //primary key

        public int OpponentID { get; set; } //id of opponent

        public DateTime Date { get; set; } //date of match
        public string Comments { get; set; } //comments of match
        public int gameID { get; set; } //id of game associated with match
        public bool Win { get; set; } //true or false for win
    }
}