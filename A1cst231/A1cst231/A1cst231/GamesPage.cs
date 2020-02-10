using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace A1cst231
{
    public class GamesPage : ContentPage
    {
        private static A1_Database database = App.Database;

        public static ListView Games = new ListView { ItemTemplate = new DataTemplate(typeof(GameCell)), RowHeight = GameCell.RowHeight };
        public static int totalChess = 0;
        public static int totalChecker = 0;
        public static int totalDominos = 0;

        public GamesPage()
        {

            

            Content = new StackLayout
            {
                Children = {
                    Games
                }
            };
        }

        protected override void OnAppearing()
        {
            Games.ItemsSource = database.GetGames();

            totalChess = database.GetGameCount(0);
            totalChess = database.GetGameCount(1);
            totalChess = database.GetGameCount(2);
        }

        public class GameCell : ViewCell
        {
            //class that will display one fruit in a list view
            public const int RowHeight = 100;

            public GameCell()
            {

                Label lblName = new Label { FontAttributes = FontAttributes.Bold };
                lblName.SetBinding(Label.TextProperty, "GameName");


                Label lblDesc = new Label { FontAttributes = FontAttributes.Bold };
                lblDesc.SetBinding(Label.TextProperty, "Description");

                Label lblRating = new Label { FontAttributes = FontAttributes.Bold };
                lblRating.SetBinding(Label.TextProperty, "Rating");

                StackLayout row2 = new StackLayout { Orientation = StackOrientation.Horizontal,Children = { lblDesc, lblRating}, HorizontalOptions = LayoutOptions.StartAndExpand };

                Label TotalMatches = new Label { Text = "# Matches:" };
                EntryCell totalMatches

                

                //Label lblNum = new Label { FontAttributes = FontAttributes.Bold, Text = "# Matches:" };



                View = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 5,
                    Padding = 5,
                    Children = { lblName, row2}
                };

            }


        }


    }


   
}