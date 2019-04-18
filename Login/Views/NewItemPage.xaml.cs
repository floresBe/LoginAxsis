using System;
using System.ComponentModel;
using Xamarin.Forms;
using Login.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Login.ViewModels;

namespace Login.Views
{
    [DesignTimeVisible(true)]
    public partial class NewItemPage : ContentPage
    {
        public Usuario Item { get; set; }

        bool isEmail { get; set; }
        bool isPassOk { get; set; }

        bool isEdit { get; set; }

        //New Usuario Constructor
        public NewItemPage()
        {
            InitializeComponent();

            Title = "New";
            Item = new Usuario();

            BindingContext = this;
        }

        //Edit Usuario Constructor
        public NewItemPage(Usuario Item)
        {
            InitializeComponent();

            Title = "Edit";
            Item.Contrasena = null;
            this.Item = Item;
            isEdit = true;

            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<ItemsViewModel, string>(this, "SendMessage", async (obj, message) =>
            {
                await DisplayAlert("Atención", message, "Ok");
            });
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (!await ValidFields())
            {
                return;
            }

            if (isEdit)
            {
                MessagingCenter.Send(this, "EditItem", Item);

            }
            else
            {
                MessagingCenter.Send(this, "AddItem", Item);
            }

            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        void Email_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            isEmail = Regex.IsMatch(Item.CorreoElectronico, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (isEmail)
            {
                eMailEntry.TextColor = Color.Default;
            }
            else
            {
                eMailEntry.TextColor = Color.Red;
            }
        }

        async Task<bool> ValidFields()
        {
            if (Item.Nombre == null)
            {
                await DisplayAlert("Atención", "Ingresar nombre de usuario.", "Ok");
                UsuarioEntry.Focus();

                return false;
            }

            if (Item.Sexo == null)
            {
                await DisplayAlert("Atención", "Seleccionar sexo.", "Ok");

                return false;
            }

            if (Item.CorreoElectronico == null)
            {
                await DisplayAlert("Atención", "Ingresar correo electónico.", "Ok");
                eMailEntry.Focus();

                return false;
            }

            if (!isEmail)
            {
                await DisplayAlert("Atención", "La estructura del correo no es la correcta.", "Ok");
                eMailEntry.Focus();

                return false;
            }

            if (Item.Contrasena == null)
            {
                await DisplayAlert("Atención", "Ingresar contraseña.", "Ok");
                ContrasenaEntry.Focus();

                return false;
            }

            isPassOk = Regex.IsMatch(Item.Contrasena, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{10,}$", RegexOptions.IgnoreCase);
            if (!isPassOk)
            {
                await DisplayAlert("Atención", "Contraseña insegura.", "Ok");
                ContrasenaEntry.Focus();
                return false;
            }

            if (RContrasenaEntry.Text == null)
            {
                await DisplayAlert("Atención", "Confirmar contraseña.", "Ok");
                RContrasenaEntry.Focus();
                return false;
            }

            if (RContrasenaEntry.Text != Item.Contrasena)
            {
                await DisplayAlert("Atención", "Las contraseñas no coinciden.", "Ok");
                ContrasenaEntry.Focus();
                return false;
            }


            return true;
        }

    }
}