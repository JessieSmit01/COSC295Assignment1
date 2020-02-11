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
        private static A1_Database database = App.Database;

       // public static ObservableCollection<Opponent> list = new ObservableCollection<Opponent>(database.GetOpponents());
        public static ListView Opponents = new ListView {  ItemTemplate = new DataTemplate(typeof(OpponentCell)), RowHeight = OpponentCell.RowHeight };



        public OpponentPage()
        {

           
            Button btnAdd = new Button { Text = "Add New Opponent" };
            btnAdd.Clicked += (sender, e) => {
                Navigation.PushAsync(new AddOpponentPage());
            };

            Opponents.ItemTapped += (sender, e) =>
            {
                Navigation.PushAsync(new MatchPage(e.Item as Opponent));
            };
            
            

            Content = new StackLayout
            {
                
                Children = {
                    Opponents,
                    btnAdd
                }
            };

           
        }

        protected override void OnAppearing()
        {
            Opponents.ItemsSource = database.GetOpponents();
        }

        public class OpponentCell : ViewCell
        {
            //class that will display one fruit in a list view
            public const int RowHeight = 55;

            public OpponentCell()
            {

                
                //BindableProperty id = new BindableProperty;
                var lblFName = new Label { FontAttributes = FontAttributes.Bold };
                lblFName.SetBinding(Label.TextProperty, "FirstName");
                var lblLName = new Label { FontAttributes = FontAttributes.Bold };
                lblLName.SetBinding(Label.TextProperty, "LastName");
                var lblAddr = new Label { FontAttributes = FontAttributes.Bold };
                lblAddr.SetBinding(Label.TextProperty, "Address");
                var lblPhone = new Label { FontAttributes = FontAttributes.Bold };
                lblPhone.SetBinding(Label.TextProperty, "Phone");
                var lblEmail = new Label { FontAttributes = FontAttributes.Bold };
                lblEmail.SetBinding(Label.TextProperty, "Email");

                

                var delete = new MenuItem { IsDestructive=true, Text="Delete"};

                delete.Clicked += (sender, e) =>
                {
                    int id = ((Opponent)this.BindingContext).ID;
                    database.DeleteOpponent(database.GetOpponent(id));
                    Opponents.ItemsSource = database.GetOpponents();
                };

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