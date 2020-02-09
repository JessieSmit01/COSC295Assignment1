using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace A1cst231
{
    public class AddOpponentPage : ContentPage
    {
        private static A1_Database database = App.Database;

        public AddOpponentPage()
        {
            EntryCell fName = new EntryCell { Label = "First Name" };
            EntryCell lName = new EntryCell { Label = "Last Name" };
            EntryCell addr = new EntryCell { Label = "Address" };
            EntryCell phone = new EntryCell { Label = "Phone" };
            EntryCell email = new EntryCell { Label = "Email" };
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

            Button btnSave = new Button();

            btnSave.Clicked += (sender, e) =>
            {
                database.SaveOpponent(new Opponent { Address = fName.Text ,Email = email.Text,FirstName=lName.Text,LastName=lName.Text, Phone=phone.Text});
                Navigation.PopAsync();
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