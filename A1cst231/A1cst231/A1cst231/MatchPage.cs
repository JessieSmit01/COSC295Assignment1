using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

            

            Match SelectedMatch = null;

            DatePicker matchDate = new DatePicker { Date = DateTime.Now };
            Picker games = new Picker { ItemsSource = database.GetGameNames() };
            
            games.SelectedIndex = GetLastGame().Result;
            
       

            ViewCell date = new ViewCell { View = matchDate};
            EntryCell comment = new EntryCell { Label = "Comment:" };
            ViewCell Game = new ViewCell { View = games};
            SwitchCell win = new SwitchCell { Text = "Win?" };
            TableView table = new TableView
            {
                Root = new TableRoot
                {
                    new TableSection
                    {
                        date, comment, Game, win
                    }
                }
            };


            Matches.ItemTapped += (sender, e) =>
            {
                SelectedMatch = (e.Item as Match);
                matchDate.Date = SelectedMatch.Date;
                comment.Text = SelectedMatch.Comments;
                games.SelectedItem = database.GetGame(SelectedMatch.GameID);
                win.On = SelectedMatch.Win;
            };

            Button btnSave = new Button { Text = "Save" };
            btnSave.Clicked += (sender, e) =>
            {
                if(SelectedMatch != null)
                {
                    database.SaveMatch(new Match { Date = matchDate.Date, Win = win.On, Comments = comment.Text, GameID = games.SelectedIndex, OpponentID = OpponentID , ID = SelectedMatch.ID});
                    SelectedMatch = null;

                    SecureStorage.SetAsync("Game", games.SelectedIndex.ToString());
                }
                else
                {
                    database.SaveMatch(new Match { Date = matchDate.Date, Win = win.On, Comments = comment.Text, GameID = games.SelectedIndex, OpponentID = OpponentID });
                    SecureStorage.SetAsync("Game", games.SelectedIndex.ToString());
                }

                Matches.ItemsSource = database.GetMatchesForOpponent(OpponentID);

            };


            Content = new StackLayout
            {
                Children = {
                    Matches, table, btnSave
                }
            };
        }

        async Task<int> GetLastGame()
        {
            string result = await SecureStorage.GetAsync("Game");
            //check if something was found
            if (result != null)
            {
                return result.Equals('0') ? 0 : result.Equals('1') ? 1 : 2;
            }
            else
            {

                return 0;
            }
        }

        protected override void OnAppearing()
        {
            Matches.ItemsSource = database.GetMatchesForOpponent(OpponentID);
        }

        public class MatchCell : ViewCell
        {
            //class that will display one fruit in a list view
            public const int RowHeight = 100;

            public MatchCell()
            {


                var lblOppName = new Label { FontAttributes = FontAttributes.Bold, Text=name };
                var lblGameID = new Label { FontAttributes = FontAttributes.Bold };
                lblGameID.SetBinding(Label.TextProperty, "GameID");
                var lblDate = new Label { FontAttributes = FontAttributes.Bold};
                lblDate.SetBinding(Label.TextProperty, "Date");
                var lblComments = new Label { FontAttributes = FontAttributes.Bold };
                lblComments.SetBinding(Label.TextProperty, "Comments");
                StackLayout row2 = new StackLayout { Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Children =
                    {
                        lblDate, lblComments
                    }
                };
                
                var win = new Switch ();
                win.SetBinding(Switch.IsToggledProperty, "Win");
                Label lblWin = new Label { Text = "Win?" };



               
                StackLayout row3 = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Children =
                    {
                       lblGameID, lblWin, win
                    }
                };

                



                View = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 5,
                    Padding = 5,
                    Children = { lblOppName, row2,row3}
                };

            }


        }
    }
}