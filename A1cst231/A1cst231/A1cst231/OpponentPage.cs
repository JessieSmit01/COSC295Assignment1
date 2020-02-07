using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace A1cst231
{
    public class OpponentPage : ContentPage
    {
        
        public OpponentPage()
        {
            A1_Database database = App.Database;
            List<Opponent> OppList = database.GetOpponents();

            ListView Opponents = new ListView { ItemsSource = OppList, ItemTemplate = new DataTemplate(typeof(OpponentCell)) };


            

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
                    A1_Database database = App.Database;
                    Opponent = new Opponent
                    {
                        Address=lblAddr.ToString(),
                        Email = lblEmail.ToString(),
                        FirstName=lblFName.ToString(),
                        

                    }
                    database.DeleteOpponent(e);
                };

                ContextActions.Add(delete);

                View = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    Padding = 5,
                    Children = { lblFName, lblLName, lblAddr, lblEmail, lblPhone }
                };

            }

           
        }
    }
}