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
            Button btnReset = new Button { Text = "Reset", BackgroundColor = Color.Red };
            btnReset.Clicked += (sender, e) =>
            {
                database.ResetTables();

                Navigation.PopToRootAsync();
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