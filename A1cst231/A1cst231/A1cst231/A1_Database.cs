using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace A1cst231
{
    public class A1_Database
    {
        readonly SQLiteConnection database;

        public A1_Database(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            //database.DropTable<fuelPurchase>(); //use to delete table
            database.CreateTable<Game>(); //Create a table using the Game ORM object **IF one does not currently exist
            database.CreateTable<Opponent>(); //Create a table using the Opponent ORM object **IF one does not currently exist
            database.CreateTable<Match>(); //Create a table using the Match ORM object **IF one does not currently exist

            //for debug
            if (database.Table<Opponent>().Count() == 0)
            {
                // configure a fuel purchase object, then save to the database
                Opponent op1 = new Opponent { Address = "123", Email = "123@gmail.com", FirstName = "John", LastName = "Smith", Phone = "0000000000" }; //leaving ID out
                SaveOpponent(op1);

            }

            if (database.Table<Game>().Count() == 0)
            {
                AddGames();

            }
            
        }

        //Game get requests
        public List<String> GetGameNames()
        {
            List<string> names = new List<string>();
            List<Game> games = database.Table<Game>().ToList();
            foreach(Game g in games)
            {
                names.Add(g.GameName);

            }
            return names;
        }

        public Game GetGame(string name)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Game>().Where(e => e.GameName == name).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        public List<Game> GetGames()
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Game>().ToList(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        public Game GetGame(int id)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Game>().Where(e => e.ID == id).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        public int GetGameCount(int id)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Match>().Where(e => e.GameID == id).Count(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        //Match Get requests
        public List<Match> GetMatchesForOpponent(int i)
        {
            return database.Table<Match>().Where(e=>e.OpponentID == i).ToList();
        }

        public void AddGames()
        {
            Game chess = new Game { GameName = "Chess", Description = "Simple grid game", Rating = 9.5 };
                Game checkers = new Game { GameName = "Checkers", Description = "Simpler grid game", Rating = 5 };
                Game dominos = new Game { GameName = "Dominoes", Description = "Blocks game", Rating = 6.75 };
                SaveGame(chess);
                SaveGame(checkers);
                SaveGame(dominos);
        }

        public Match GetMatch(int id)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Match>().Where(e => e.ID == id).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        public string GetGameName(int id)
        {
            Game selected = database.Table<Game>().Where(e=>e.ID == id).First();
            return selected.GameName;
        }

        public string GetOpponentName(int id)
        {
            Opponent selected = database.Table<Opponent>().Where(e => e.ID == id).First();
            return selected.FirstName + " " + selected.LastName;
        }


        //Opponent Get requests
        public List<Opponent> GetOpponents()
        {
            return database.Table<Opponent>().ToList();
        }

        public Opponent GetOpponent(int id)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Opponent>().Where(e => e.ID == id).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        public int SaveOpponent(Opponent item)
        {
            if (item.ID != 0) //has already been saved once (has an ID)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int SaveMatch(Match item)
        {
            if (item.ID != 0) //has already been saved once (has an ID)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }

        public int SaveGame(Game item)
        {
            return database.Insert(item);
        }

        public int DeleteOpponent(Opponent item)
        {
            
                return database.Delete(item);
            
        }

        public void ResetTables()
        {
            //drop all tables
            database.DropTable<Game>();
            database.DropTable<Opponent>();
            database.DropTable<Match>();

            //recreate all tables
            database.CreateTable<Game>(); //Create a table using the Game ORM object **IF one does not currently exist
            database.CreateTable<Opponent>(); //Create a table using the Opponent ORM object **IF one does not currently exist
            database.CreateTable<Match>(); //Create a table using the Match ORM object **IF one does not currently exist

            //add the games into the game table with default values
            AddGames();
        }
    }
}
