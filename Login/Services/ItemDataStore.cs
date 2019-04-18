using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Login.Models;
using SQLite; 
using System.IO;

namespace Login.Services
{
    public class ItemDataStore : IDataStore<Item>
    {
        string dbPath;
        SQLiteConnection db;

        public ItemDataStore()
        {
            try
            {
                dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{App.dataBaseName}.db3");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear MockDataStore. " + ex.Message);
            }
        }

        public async Task<bool> AddAsync(Item item)
        {
            try
            {
                db = new SQLiteConnection(dbPath);
                db.Insert(item);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar items. " + ex.Message);
            }
            finally
            {
                db = null;
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Item item)
        {
            try
            {
                db = new SQLiteConnection(dbPath);
                db.Execute($"UPDATE Item" +
                    $"SET Text = {item.Text} WHERE Id = {item.Id}");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar item. " + ex.Message);
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
                db = new SQLiteConnection(dbPath);
                db.Execute($"DELETE FROM Item WHERE Id = {id}");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar item por Id. " + ex.Message);
            }
            finally
            {
                await Task.Delay(1);
                db = null;
            }

            return await Task.FromResult(true);
        }

        public async Task<Item> GetAsync(int id)
        {
            Item item = null;
            try
            {
                db = new SQLiteConnection(dbPath);
                item = db.Query<Item>($"SELECT * FROM Item WHERE Id = {id}").FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener item por Id. " + ex.Message);
            }
            finally
            {
                await Task.Delay(1);
                db = null;
            }
            return item;
        }

        public async Task<IEnumerable<Item>> GetAsync(bool forceRefresh = false)
        {
            List<Item> items;
            items = new List<Item>();
            try
            {
                db = new SQLiteConnection(dbPath);
                var mockItems = db.Table<Item>().ToList();

                foreach (var item in mockItems)
                {
                    items.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener items. " + ex.Message);
            }
            finally
            {
                db = null;
            }

            return await Task.FromResult(items);
        } 

        public Task<bool> ExistsAsync(Item item)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetAsync(string key)
        {
            throw new NotImplementedException();
        }
    }
}