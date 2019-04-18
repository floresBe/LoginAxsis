using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Models;
using SQLite; 
using System.IO;

namespace Login.Services
{
    public class UsuarioDataStore : IDataStore<Usuario>
    {
        string dbPath;
        SQLiteConnection db;

        public UsuarioDataStore()
        {
            try
            {            
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),$"{App.dataBaseName}.db3");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear DataStore. " + ex.Message);
            }
        }

        public async Task<bool> AddAsync(Usuario usuario)
        {
            try
            {
                usuario.FechaCreacion = DateTime.Now.ToString();
                db = new SQLiteConnection(dbPath);
                db.Insert(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar usuarios. " + ex.Message);
            }
            finally
            {
                db = null;
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            try
            {
                string query = "UPDATE Usuario " +
                    $"SET Nombre = '{usuario.Nombre}' " +
                    $", Contrasena = '{usuario.Contrasena}' " +
                    $", Sexo = '{usuario.Sexo}' " +
                    $", CorreoElectronico = '{usuario.CorreoElectronico}' " +
                    $", Activo = '{usuario.Activo}' " +
                    $"WHERE Id = '{usuario.Id}'";

                db = new SQLiteConnection(dbPath);
                db.Execute(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario. " + ex.Message);
            }
            finally
            {
                await Task.Delay(1);
                db = null;
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                string query = "UPDATE Usuario " +
                    "SET Activo = 'false' " +
                    $"WHERE Id = '{id}'";

                db = new SQLiteConnection(dbPath);
                db.Execute(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario por Id. " + ex.Message);
            }
            finally
            {
                await Task.Delay(1);
                db = null;
            }

            return await Task.FromResult(true);
        }

        public async Task<Usuario> GetAsync(int id)
        {
            Usuario usuario = null;
            try
            {
                db = new SQLiteConnection(dbPath);
                usuario = db.Query<Usuario>($"SELECT * FROM Usuario WHERE Id = {id}").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por Id. " + ex.Message);
            }
            finally
            {
                await Task.Delay(1);
                db = null;
            }
            return usuario;
        }

        public async Task<Usuario> GetAsync(string nombre)
        {
            Usuario usuario = null;
            try
            {
                db = new SQLiteConnection(dbPath);
                usuario = db.Query<Usuario>($"SELECT * FROM Usuario WHERE Nombre = '{nombre}'").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por Nombre. " + ex.Message);
            }
            finally
            {
                await Task.Delay(1);
                db = null;
            }
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetAsync(bool forceRefresh = false)
        {
            List<Usuario> usuarios;
            usuarios = new List<Usuario>();
            try
            {
                db = new SQLiteConnection(dbPath);
                var mockUsuarios = db.Table<Usuario>().ToList();

                foreach (var usuario in mockUsuarios)
                {
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios. " + ex.Message);
            }
            finally
            {
                db = null;
            }

            return await Task.FromResult(usuarios);
        }

        public async Task<bool> ExistsAsync(Usuario usuario)
        {
            List<Usuario> usuarios = null;
            try
            {
                db = new SQLiteConnection(dbPath);
                usuarios = db.Query<Usuario>($"SELECT * FROM Usuario " +
                	       $"WHERE Nombre = '{usuario.Nombre}' OR " +
                	       $"CorreoElectronico = '{usuario.CorreoElectronico}' " ).ToList();

                if (usuarios.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por Id. " + ex.Message);
            }
            finally
            {
                await Task.Delay(1);
                db = null;
            }
            return false;
        }

         
    }
}