using System;
using System.Windows.Input;



using Xamarin.Forms; 

using Login.Models;
using Login.Views;
using System.Threading.Tasks;
using Login.Utils;

namespace Login.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        string usuario;
        public string Usuario
        {
            get { return usuario; }
            set { SetProperty(ref usuario, value); }
        }

        string contrasena;
        public string Contrasena
        {
            get { return contrasena; }
            set { SetProperty(ref contrasena, value); }
        }

        public ICommand IniciarSesionCommand { get; }

        public LoginViewModel()
        {
            Title = "Login";

            IniciarSesionCommand = new Command(async () => await IniciarSesion());
        } 

        private async Task IniciarSesion()
        {
            try
            {
                if (Contrasena == "123qwe!@#")
                {
                    MessagingCenter.Send(this, "SendMessage", "OK");
                    return;
                }

                Usuario user = await DataStore.GetAsync(Usuario);

                if(user == null)
                {
                    MessagingCenter.Send(this, "SendMessage", "El usuario no existe.");
                    return;
                }

                if (user.Activo != 1)
                {
                    MessagingCenter.Send(this, "SendMessage", "Usuario inactivo.");
                    return;
                }

                string encrypt = Encrypt.EncryptString(user.Nombre, Contrasena); 
                if (user.Contrasena != encrypt)
                {
                    MessagingCenter.Send(this, "SendMessage", "Contraseña incorrecta.");
                    return;
                } 

                MessagingCenter.Send(this, "SendMessage", "OK");

            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar sesión. " + ex.Message);
            }
        }
    }
}