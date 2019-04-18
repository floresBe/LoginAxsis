using System;
using System.ComponentModel;
using Login.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Login.Views
{ 
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<LoginViewModel, string>(this, "SendMessage", async (obj, message) =>
            { 
                if (message == "OK")
                {
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await DisplayAlert("Atención", message, "Ok");
                }
            });
        }
    }
}