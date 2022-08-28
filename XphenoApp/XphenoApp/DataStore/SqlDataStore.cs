using System;
using SQLite;
using XphenoApp.Exceptions;

namespace XphenoApp.DataStore
{
    public sealed class SqlDataStore
    {
        private readonly SQLiteAsyncConnection database;
        private static Lazy<SqlDataStore> lazy = null;

        public static SqlDataStore SharedInstance
        {
            get
            {
                if (lazy == null)
                {
                    throw new InstanceNotCreatedException();
                }
                return lazy.Value;
            }
        }

        public static void CreateSharedDataStore(string path)
        {
            if (lazy == null)
            {
                lazy = new Lazy<SqlDataStore>(() => new SqlDataStore(path));
            }
        }

        private SqlDataStore(string path)
        {
            database = new SQLiteAsyncConnection(path);
        }

        public SQLiteAsyncConnection Database
        {
            get
            {
                return database;
            }
        }
    }
}

