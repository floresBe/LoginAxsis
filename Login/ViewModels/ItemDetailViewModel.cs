using System;
using System.Threading.Tasks;
using Login.Models;
using Login.Utils;
using Login.Views;
using Xamarin.Forms;

namespace Login.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        Usuario item;
        public Usuario Item
        {
            get { return item; }
            set 
            { 
                    SetProperty(ref item, value);
                    if (value.Activo == 1)
                    {
                        Activo = "SI";
                    }
                    else
                    {
                        Activo = "NO";
                    }
            }
        }


        string activo;
        public string Activo 
        {
            get { return activo; }
            set 
            { 
                SetProperty(ref activo, value);
            }
        }

        public ItemDetailViewModel(Usuario item)
        {
            Title = item.Nombre;

            Item = item;


            MessagingCenter.Subscribe<NewItemPage, Usuario>(this, "EditItem", async (obj, EditItem) =>
            {
                var editItem = EditItem as Usuario;

                string encrypt = Encrypt.EncryptString(editItem.Nombre, editItem.Contrasena);
                item.Contrasena = encrypt; 

                if (await DataStore.UpdateAsync(editItem))
                {
                    Item = editItem;
                }

            });

            MessagingCenter.Subscribe<ItemDetailPage, String>(this, "DeleteItem", async (obj, delete) =>
            { 
                if (await DataStore.DeleteAsync(Item.Id))
                {
                    Item.Activo = 0;
                    Activo = "NO";
                }

            });
        }


    }
}
