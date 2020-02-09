using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace A1cst231
{

    public class Opponent
    {
        [PrimaryKey, AutoIncrement] //tell SQLite that this will be the primary key and it will autoincrement
        public int ID { get; set; }

        public string FirstName { get; set; } //opponent first name
        public string LastName { get; set; } //opponent last name
        public string Address { get; set; } //opponent address
        public string Phone { get; set; } //opponent phone number
        public string Email { get; set; } //opponent Email


    }
}
