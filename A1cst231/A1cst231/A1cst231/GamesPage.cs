using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace A1cst231
{
    public class GamesPage : ContentPage
    {
        //pointer to the databse
        private static A1_Database database = App.Database;

        //list view to hold all games
        public static ListView Games = new ListView { ItemTemplate = new DataTemplate(typeof(GameCell)), RowHeight = GameCell.RowHeight };
   


        public GamesPage()
        {
            //Set the itemsource og the list view
            Games.ItemsSource = database.GetGames();

            Content = new StackLayout
            {
                Children = {
                    Games
                }
            };
        }

        /// <summary>
        /// On appearing, set the itemsource
        /// </summary>
        protected override void OnAppearing()
        {
            Games.ItemsSource = database.GetGames();

        }

        /// <summary>
        /// This class will hold a game cell
        /// </summary>
        public class GameCell : ViewCell
        {
            //request row height of 100
            public const int RowHeight = 100;
            Label TotalMatches = new Label { Text = "# Matches:" };
            public GameCell()
            {
                //create label for name
                Label lblName = new Label { FontAttributes = FontAttributes.Bold };
                lblName.SetBinding(Label.TextProperty, "GameName");

                //label for description
                Label lblDesc = new Label { FontAttributes = FontAttributes.Bold };
                lblDesc.SetBinding(Label.TextProperty, "Description");

                //Label for rating
                Label lblRating = new Label { FontAttributes = FontAttributes.Bold };
                lblRating.SetBinding(Label.TextProperty, "Rating");

                //stack layout for row 2
                StackLayout row2 = new StackLayout { Orientation = StackOrientation.Horizontal,Children = { lblDesc, lblRating}, HorizontalOptions = LayoutOptions.StartAndExpand };

                //add each element to the view
                View = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Spacing = 5,
                        Padding = 5,
                        Children = { lblName, row2, TotalMatches}
                    };

            }

            /// <summary>
            /// This will fire when the binding context is changed
            /// </summary>
            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();

                //if the binding context is not null, update total matched for this cell by calling the method in the database
                if(this.BindingContext != null)
                {
                    TotalMatches.Text += database.GetGameCount(((Game)this.BindingContext).ID);

                }

            }


        }

        


    }


   
}