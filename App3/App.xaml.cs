using System;
using System.IO;
using Xamarin.Forms;
using NavPageSample.page;


namespace NavPageSample
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)                                                                                                   //DBƒtƒ@ƒCƒ‹–¼
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "R03TEAM04.db"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new TabbedPage1();

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
}
