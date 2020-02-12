using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace A1cst231
{
    public class A1_Database
    {
        //create an SQLiteConnection to use for database calls
        readonly SQLiteConnection database;

        public A1_Database(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            //database.DropTable<fuelPurchase>(); //use to delete table
            database.CreateTable<Game>(); //Create a table using the Game ORM object **IF one does not currently exist
            database.CreateTable<Opponent>(); //Create a table using the Opponent ORM object **IF one does not currently exist
            database.CreateTable<Match>(); //Create a table using the Match ORM object **IF one does not currently exist

            

            //check if the games table is empty, if it is, add the games.
            if (database.Table<Game>().Count() == 0)
            {
                AddGames();

            }
            
        }

        /// <summary>
        /// This method will be used to get each game name in the databse
        /// </summary>
        /// <returns></returns>
        public List<String> GetGameNames()
        {
            List<string> names = new List<string>(); 
            List<Game> games = database.Table<Game>().ToList();
            foreach(Game g in games) //foreach game in the game list add its name to the names string list
            {
                names.Add(g.GameName);

            }
            return names; //return the list of names
        }

        /// <summary>
        /// This method will return the game object whose name matches the name passed in
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Game GetGame(string name)
        {
            //return the game with the matching name of the name passed in
            return database.Table<Game>().Where(e => e.GameName == name).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        /// <summary>
        /// This method will return all games in the database
        /// </summary>
        /// <returns></returns>
        public List<Game> GetGames()
        {
            //return all games 
            return database.Table<Game>().ToList(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        /// <summary>
        /// This method will return the game with an id matching to the int passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Game GetGame(int id)
        {
            //Return game with the id if the in passed in
            return database.Table<Game>().Where(e => e.ID == id).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        /// <summary>
        /// this method will return the count of total macthes where GameID is equal to the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetGameCount(int id)
        {
            //return the count of total macthes where GameID is equal to the id passed in
            return database.Table<Match>().Where(e => e.GameID == id).Count(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        /// <summary>
        /// This method will return all matches where OpponentID macthes id passed in
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<Match> GetMatchesForOpponent(int i)
        {
            
            return database.Table<Match>().Where(e=>e.OpponentID == i).ToList();
        }

        /// <summary>
        /// This method will add the default games and their values to the database
        /// </summary>
        public void AddGames()
        {
            Game chess = new Game { GameName = "Chess", Description = "Simple grid game", Rating = 9.5 };
                Game checkers = new Game { GameName = "Checkers", Description = "Simpler grid game", Rating = 5 };
                Game dominos = new Game { GameName = "Dominoes", Description = "Blocks game", Rating = 6.75 };
                SaveGame(chess);
                SaveGame(checkers);
                SaveGame(dominos);
        }

        //This method will return the match with the given id
        public Match GetMatch(int id)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Match>().Where(e => e.ID == id).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        //This method will return the Game name matching the id passed in
        public string GetGameName(int id)
        {
            Game selected = database.Table<Game>().Where(e=>e.ID == id).First();
            return selected.GameName;
        }

        /// <summary>
        /// This method will return the opponent name matching the id passed in
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetOpponentName(int id)
        {
            Opponent selected = database.Table<Opponent>().Where(e => e.ID == id).First();
            return selected.FirstName + " " + selected.LastName;
        }


        //This method will return all opponents
        public List<Opponent> GetOpponents()
        {
            return database.Table<Opponent>().ToList();
        }


        //This method will get an opponent with the matching id passed in
        public Opponent GetOpponent(int id)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Opponent>().Where(e => e.ID == id).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        /// <summary>
        /// This method will save the passed in opponent to the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int SaveOpponent(Opponent item)
        {
            //opponent exists, just update the opponent
            if (item.ID != 0) //has already been saved once (has an ID)
            {
                return database.Update(item);
            }
            else //new oppoennt, save them
            {
                return database.Insert(item);
            }
        }

        /// <summary>
        /// This method will save the match passed into the method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int SaveMatch(Match item)
        {
            if (item.ID != 0) //has already been saved once (has an ID)
            {
                return database.Update(item);
            }
            else //new opponent, save them
            {
                return database.Insert(item);
            }
        }

        /// <summary>
        /// This method will save the game passed in to the method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int SaveGame(Game item)
        {
            return database.Insert(item);
        }

        /// <summary>
        /// This method will be used to delete the opponent that is passed in to the method
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int DeleteOpponent(Opponent item)
        {
            
                return database.Delete(item);
            
        }

        /// <summary>
        /// This method will be used to drop all tables and creeate them again, also loads in the default games
        /// </summary>
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
