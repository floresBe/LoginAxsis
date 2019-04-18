using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Login.Services;
using Login.Views;

namespace Login
{
    public partial class App : Application
    {
        public static string dataBaseName = "Usuarios";
 
        public App()
        {
            InitializeComponent();

            DataAccess.Create();
            DependencyService.Register<ItemDataStore>();

            MainPage = new LoginPage();
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
