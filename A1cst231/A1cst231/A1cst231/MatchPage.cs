using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace A1cst231
{
    public class MatchPage : ContentPage
    {
        private static A1_Database database = App.Database;

        public static ListView Matches = new ListView { ItemTemplate = new DataTemplate(typeof(MatchCell)), RowHeight = MatchCell.RowHeight };

        public static int OpponentID = 0;

        public static string name = "";

        public MatchPage(Opponent opponent)
        {
            OpponentID = opponent.ID;
            name = opponent.FirstName + " " + opponent.LastName;
            Content = new StackLayout
            {
                Children = {
                    Matches
                }
            };
        }

        protected override void OnAppearing()
        {
            Matches.ItemsSource = database.GetMatchesForOpponent(OpponentID);
        }

        public class MatchCell : ViewCell
        {
            //class that will display one fruit in a list view
            public const int RowHeight = 55;

            public MatchCell()
            {


                var lblOppName = new Label { FontAttributes = FontAttributes.Bold, Text=name };
                var lblDate = new Label { FontAttributes = FontAttributes.Bold, Text = name };
                lblDate.SetBinding(Label.TextProperty, "Date");







                View = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    Padding = 5,
                    Children = { lblOppName}
                };

            }


        }
    }
}