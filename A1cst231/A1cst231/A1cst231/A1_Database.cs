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

        }

        //Game get requests
        public List<Game> GetGames()
        {
            return database.Table<Game>().ToList();
        }

        public Game GetGame(int id)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Game>().Where(e => e.ID == id).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
        }

        //Match Get requests
        public List<Match> GetMatches()
        {
            return database.Table<Match>().ToList();
        }

        public Match GetMatch(int id)
        {
            //Use LINQ and a lambda expression to find an item
            return database.Table<Match>().Where(e => e.ID == id).FirstOrDefault(); //"i" is each object in the collection, being evaliated against the passed in parameter ;
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
    }
}
