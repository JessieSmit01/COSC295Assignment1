using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace A1cst231
{
    public class OpponentPage : ContentPage
    {
        //a pointer to the App database
        private static A1_Database database = App.Database;

        //The listview that stores all opponents
        public static ListView Opponents = new ListView {  ItemTemplate = new DataTemplate(typeof(OpponentCell)), RowHeight = OpponentCell.RowHeight, HeightRequest = 700 };



        public OpponentPage()
        {

           //the add button
            Button btnAdd = new Button { Text = "Add New Opponent" };
            //when button add is clicked
            btnAdd.Clicked += (sender, e) => {
                //open the add opponent page
                Navigation.PushAsync(new AddOpponentPage());
            };

            //when a list view item is tapped
            Opponents.ItemTapped += (sender, e) =>
            {
                //open the opponent page
                Navigation.PushAsync(new MatchPage(e.Item as Opponent));
            };
            
            
            //add items to the layout
            Content = new StackLayout
            {
                
                Children = {
                    Opponents,
                    btnAdd
                }
            };

           
        }

        //When the page appears fill tge list view with opponents from the database
        protected override void OnAppearing()
        {
            Opponents.ItemsSource = database.GetOpponents();
        }

        //class that will display one opponent in a list view
        public class OpponentCell : ViewCell
        {
            //height requested per row
            public const int RowHeight = 55;

            public OpponentCell()
            {

                
                //label for last name
                var lblFName = new Label { FontAttributes = FontAttributes.Bold };
                //bind first name
                lblFName.SetBinding(Label.TextProperty, "FirstName");
                //label for last name
                var lblLName = new Label { FontAttributes = FontAttributes.Bold };
                //bind last name
                lblLName.SetBinding(Label.TextProperty, "LastName");
                //label for address
                var lblAddr = new Label { FontAttributes = FontAttributes.Bold };
                //bind address
                lblAddr.SetBinding(Label.TextProperty, "Address");
                //label for phone number
                var lblPhone = new Label { FontAttributes = FontAttributes.Bold };
                //bind phone number
                lblPhone.SetBinding(Label.TextProperty, "Phone");
                //label for email
                var lblEmail = new Label { FontAttributes = FontAttributes.Bold };
                //bind email
                lblEmail.SetBinding(Label.TextProperty, "Email");

                
                //add a delete button when the item is swiped on iphone or long held on android
                var delete = new MenuItem { IsDestructive=true, Text="Delete"};

                //when delete button is clicked
                delete.Clicked += (sender, e) =>
                {
                    //get the id from current context
                    int id = ((Opponent)this.BindingContext).ID;
                    //delete the opponent from the database
                    database.DeleteOpponent(database.GetOpponent(id));
                    //reload the listview with opponents
                    Opponents.ItemsSource = database.GetOpponents();
                };
                //add delete button to context
                ContextActions.Add(delete);

                View = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Spacing = 5,
                    Padding = 5,
                    Children = { lblFName, lblLName, lblAddr, lblEmail, lblPhone }
                };

            }

           
        }
    }
}