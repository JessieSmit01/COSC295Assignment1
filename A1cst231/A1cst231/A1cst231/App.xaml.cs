using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace A1cst231
{
    public partial class App : Application
    {
        static A1_Database database;

        public static A1_Database Database
        {
            //give access to whole application to the database
            get
            {
                if (database == null)//check if database is connected
                {
                    //The dependency service allows us to get platform instances of classes
                    database = new A1_Database(DependencyService.Get<IFileHelper>().GetLocalFilePath("A1_SQLite.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();


            //ToolbarItem tbGames = new ToolbarItem { Text = "Games", Order = ToolbarItemOrder.Primary };
            //ToolbarItem tbSett = new ToolbarItem { Text = "Settings", Order = ToolbarItemOrder.Primary };

           
            

            MainPage = new NavigationPage(new OpponentPage());
            ToolbarItem tbGames = new ToolbarItem { Text = "Games", Order = ToolbarItemOrder.Primary };
            ToolbarItem tbSett = new ToolbarItem { Text = "Settings", Order = ToolbarItemOrder.Primary };

            MainPage.ToolbarItems.Add(tbGames);
           MainPage.ToolbarItems.Add(tbSett);

            tbGames.Clicked += (sender, e) =>
            {
                MainPage.Navigation.PushAsync(new GamesPage());
            };

            tbSett.Clicked += (sender, e) =>
            {
                MainPage.Navigation.PushAsync(new SettingPage());
            };



        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public interface IFileHelper //this will be used to get a platform specfic file location
    {
        string GetLocalFilePath(string filename);
    }
}
