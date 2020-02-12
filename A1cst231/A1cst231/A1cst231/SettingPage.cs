using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace A1cst231
{
    public class SettingPage : ContentPage
    {
        private static A1_Database database = App.Database;
        public SettingPage()
        {
            //create a button with text of reset
            Button btnReset = new Button { Text = "Reset", BackgroundColor = Color.Red };
            //on clicked, reset the tables
            btnReset.Clicked += (sender, e) =>
            {
                database.ResetTables();

                Navigation.PopToRootAsync(); //pop back to main page
            };

            Content = new StackLayout
            {
                Children = {
                    btnReset
                }
            };
        }
    }
}