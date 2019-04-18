using System;
using SQLite;

namespace Login.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }  
        public string CorreoElectronico { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public int Activo { get; set; }
        public string Sexo { get; set; }
        public string FechaCreacion { get; set; }

        public Usuario()
        {
            Activo = 1;
        }
   }
}
