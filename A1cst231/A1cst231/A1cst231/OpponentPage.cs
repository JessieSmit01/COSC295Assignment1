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

        public static ObservableCollection<Opponent> list = new ObservableCollection<Opponent>(database.GetOpponents());



        public OpponentPage()
        {

            ListView Opponents = new ListView { ItemsSource = list, ItemTemplate = new DataTemplate(typeof(OpponentCell))};

            
            

            Content = new StackLayout
            {
                
                Children = {
                    Opponents
                }
            };

           
        }

        public class OpponentCell : ViewCell
        {
            //class that will display one todo in a list view
            public const int RowHeight = 100;

            public OpponentCell()
            {

                var id = new Label();
                id.SetBinding(Label.TextProperty, "ID");
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
                lblLName.SetBinding(Label.TextProperty, "Email");

                

                var delete = new MenuItem { IsDestructive=true, Text="Delete"};

                delete.Clicked += (sender, e) =>
                {
                   
                    list.Remove((Opponent)this.BindingContext);
                    database.DeleteOpponent(database.GetOpponent(((Opponent)this.BindingContext).ID));
                    
                    
                };

                ContextActions.Add(delete);

                View = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    Padding = 5,
                    Children = {id, lblFName, lblLName, lblAddr, lblEmail, lblPhone }
                };

            }

           
        }
    }
}