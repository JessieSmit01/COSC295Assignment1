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
        //pointer to the database
        private static A1_Database database = App.Database;
        //list view to hold matches
        public static ListView Matches = new ListView { ItemTemplate = new DataTemplate(typeof(MatchCell)), RowHeight = MatchCell.RowHeight };
        //need to keep track of opponent passed in
        public static int OpponentID = 0;

        public static string name = "";
       

        public MatchPage(Opponent opponent)
        {

            //set the opponent name
            OpponentID = opponent.ID;

            //set the name of the opponent
            name = opponent.FirstName + " " + opponent.LastName;

            
            //selected match is null
            Match SelectedMatch = null;

            //default date is today
            DatePicker matchDate = new DatePicker { Date = DateTime.Now };
            //create a picker for each game name
            Picker games = new Picker { ItemsSource = database.GetGameNames() };

            //set the selected index to the last index selected
            games.SelectedIndex = GetLastGame().Result;
            
       
            //create fields to place in the table to create a new match or update an existing match
            ViewCell date = new ViewCell { View = matchDate};
            EntryCell comment = new EntryCell { Label = "Comment:" };
            ViewCell Game = new ViewCell { View = games};
            SwitchCell win = new SwitchCell { Text = "Win?" };

            //add elements to the table
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

            //when a list view item is tapped, fill table elements with the cells info for updating
            Matches.ItemTapped += (sender, e) =>
            {
                //update each table field to the selected matches info
                SelectedMatch = (e.Item as Match);
                matchDate.Date = SelectedMatch.Date;
                comment.Text = SelectedMatch.Comments;
                games.SelectedIndex = SelectedMatch.GameID + 1;
                win.On = SelectedMatch.Win;
            };

            //Create a save button to save the match
            Button btnSave = new Button { Text = "Save" };
            //on button clicked
            btnSave.Clicked += (sender, e) =>
            {
                //if selected match is not null (new match)
                if(SelectedMatch != null)
                {
                    //save the match to the database passing in all info
                    database.SaveMatch(new Match { Date = matchDate.Date, Win = win.On, Comments = comment.Text, GameID = games.SelectedIndex + 1, OpponentID = OpponentID , ID = SelectedMatch.ID});

                    SelectedMatch = null; //set selcted match back to null

                    SecureStorage.SetAsync("Game", games.SelectedIndex + ""); //set the last value 

                    //reset each field
                   
                    matchDate.Date = DateTime.Now;
                    comment.Text = "";
                    
                    win.On = false;
                }
                else //sected match is not null
                {
                    //save the match to the database passing in all info but ID
                    database.SaveMatch(new Match { Date = matchDate.Date, Win = win.On, Comments = comment.Text, GameID = games.SelectedIndex + 1, OpponentID = OpponentID });
                    SecureStorage.SetAsync("Game", games.SelectedIndex + "");

                    //reset each field
                    matchDate.Date = DateTime.Now;
                    comment.Text = "";

                    win.On = false;
                }
                //set the itemsource for the matches list view
                Matches.ItemsSource = database.GetMatchesForOpponent(OpponentID);

            };


            Content = new StackLayout
            {
                Children = {
                    Matches, table, btnSave
                }
            };
        }

        /// <summary>
        /// This method will get the value form secure storage used to keep track of last selected game
        /// </summary>
        /// <returns></returns>
        async Task<int> GetLastGame()
        {
            //get the value of the last selcted game
            string result = await SecureStorage.GetAsync("Game");
            //check if something was found
            if (result != null) //if there was a value
            {
                //return the int associated with the string value
                return result.Equals('0') ? 0 : result.Equals('1') ? 1 : 2;
            }
            else
            {
                //return 0 default
                return 0;
            }
        }

        /// <summary>
        /// When the page appears set the item source of the list view to the database.getNatchesForOpponent method return value
        /// </summary>
        protected override void OnAppearing()
        {
            Matches.ItemsSource = database.GetMatchesForOpponent(OpponentID);
        }

        public class MatchCell : ViewCell
        {
            //class that will display one fruit in a list view
            public const int RowHeight = 100;
            //label for game id
            Label lblGameID = new Label { FontAttributes = FontAttributes.Bold };
            //label for opponent name
            Label lblOppName = new Label { FontAttributes = FontAttributes.Bold };

            public MatchCell()
            {


                //label for date
                var lblDate = new Label { FontAttributes = FontAttributes.Bold};
                //bind label for date
                lblDate.SetBinding(Label.TextProperty, "Date");
                //label for comments
                var lblComments = new Label { FontAttributes = FontAttributes.Bold };
                //bind label sfor comments
                lblComments.SetBinding(Label.TextProperty, "Comments");

                //create  ahorizontal stacklayout for row 2 of the cell
                StackLayout row2 = new StackLayout { Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Children =
                    {
                        lblDate, lblComments
                    }
                };
                //create a switch
                Switch win = new Switch ();
                win.SetBinding(Switch.IsToggledProperty, "Win"); //bind toggled property
                

                Label lblWin = new Label { Text = "Win?" }; //create a label



               //create the third row in the cell
                StackLayout row3 = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal, //orientation to horizontal
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

            //On binding context changed, set the lblGameID to the game name taken form the database, 
            //set Opp Name to the name taken form the database using Opponent ID
            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();

                if (this.BindingContext != null)
                {
                    lblGameID.Text = database.GetGameName(((Match)this.BindingContext).GameID);
                    lblOppName.Text = database.GetOpponentName(((Match)this.BindingContext).OpponentID);

                }

            }


        }
    }
}