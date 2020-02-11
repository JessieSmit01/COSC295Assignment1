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
   


        public GamesPage()
        {

            Games.ItemsSource = database.GetGames();

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

        }

        public class GameCell : ViewCell
        {
            //class that will display one fruit in a list view
            public const int RowHeight = 100;
            Label TotalMatches = new Label { Text = "# Matches:" };
            public GameCell()
            {

                Label lblName = new Label { FontAttributes = FontAttributes.Bold };
                lblName.SetBinding(Label.TextProperty, "GameName");


                Label lblDesc = new Label { FontAttributes = FontAttributes.Bold };
                lblDesc.SetBinding(Label.TextProperty, "Description");

                Label lblRating = new Label { FontAttributes = FontAttributes.Bold };
                lblRating.SetBinding(Label.TextProperty, "Rating");

                StackLayout row2 = new StackLayout { Orientation = StackOrientation.Horizontal,Children = { lblDesc, lblRating}, HorizontalOptions = LayoutOptions.StartAndExpand };

                
                

                
                


            View = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 5,
                    Padding = 5,
                    Children = { lblName, row2, TotalMatches}
                };

            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();


                TotalMatches.Text += database.GetGameCount(((Game)this.BindingContext).ID) ;

            }


        }

        


    }


   
}