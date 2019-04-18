using System;
using SQLite;
using System.IO;
using Login.Models;

namespace Login.Services
{
    public static class DataAccess
    {
        static string dbPath;

        public static void Create()
        {
            CreateDataBase();
            CreateTables();
        }

        static void CreateDataBase()
        {
            Console.WriteLine("Creating database, if it doesn't already exist");
            dbPath = Path.Combine(
                 Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                 $"{App.dataBaseName}.db3");
        }

        static void CreateTables()
        {
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Usuario>();
        }
    }
}
