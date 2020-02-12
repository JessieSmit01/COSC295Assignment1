using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace A1cst231
{
    public class AddOpponentPage : ContentPage
    {
        //pointer to the app database
        private static A1_Database database = App.Database;

        public AddOpponentPage()
        {
            //create entry cells fro each opponent field
            EntryCell fName = new EntryCell { Label = "First Name" };
            EntryCell lName = new EntryCell { Label = "Last Name" };
            EntryCell addr = new EntryCell { Label = "Address" };
            EntryCell phone = new EntryCell { Label = "Phone" };
            EntryCell email = new EntryCell { Label = "Email" };

            //create a table to hold those entry cells
            TableView table = new TableView
            {
                Root = new TableRoot
                {
                    new TableSection
                    {
                        fName,
                        lName,
                        addr,
                        phone,
                        email
                    }
                }
            };
            //create a button to save the opponent
            Button btnSave = new Button { Text = "Save" };

           
            //on save button clicked
            btnSave.Clicked += (sender, e) =>
            { //save the opponent using info passed into table
                database.SaveOpponent(new Opponent { Address = addr.Text ,Email = email.Text,FirstName=fName.Text,LastName=lName.Text, Phone=phone.Text});
                Navigation.PopAsync(); //pop the page off the stack
            };

            Content = new StackLayout
            {
                
                Children = {
                    table,
                    btnSave
                }
            };
        }
    }
}