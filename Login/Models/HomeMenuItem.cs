using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Login.Models
{
    public enum MenuItemType
    {
        Usuarios,
        Acerca,
        Salir
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
 
    }
}
