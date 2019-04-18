using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Login.Models;
using Login.Views;

namespace Login.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Usuario> Usuarios { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        { 
            Title = "Usuarios";
            Usuarios = new ObservableCollection<Usuario>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand()); 

            MessagingCenter.Subscribe<NewItemPage, Usuario>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Usuario;

                if (await DataStore.ExistsAsync(newItem))
                {
                    MessagingCenter.Send(this, "SendMessage", "El usuario ya existe.");
                    return;
                }

                if (await DataStore.AddAsync(newItem))
                {
                    Usuarios.Add(newItem);
                }

            }); 
        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Usuarios.Clear();
                var items = await DataStore.GetAsync(true);
                foreach (var item in items)
                {
                    Usuarios.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }



    }
}