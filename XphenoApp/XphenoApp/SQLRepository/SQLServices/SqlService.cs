using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using XphenoApp.DataStore;
using XphenoApp.SQLRepository.ISQLServices;

namespace XphenoApp.SQLRepository.SQLServices
{
    public class SqlService : ISqlService
    {
        /// <summary>
        /// Deletes the specific row whos primary key is passed as an argument
        /// </summary>
        /// <typeparam name="T"> Table that you'd like to delete data from </typeparam>
        /// <param name="primaryKey"> primary key of that row entry </param>
        /// <returns> a boolean that represents if the deletion was sucessful </returns>
        public async Task<bool> DeleteDataAsync<T>(object primaryKey)
        {
            return await SqlDataStore.SharedInstance.Database.DeleteAsync<T>(primaryKey) != 0;
        }

        /// <summary>
        /// Get all data of type
        /// </summary>
        /// <typeparam name="T"> Table that you want to query </typeparam>
        /// <returns> list of your data if exists or an empty collection </returns>
        public async Task<List<T>> GetAllDataAsync<T>() where T : new()
        {
            return await SqlDataStore.SharedInstance.Database.Table<T>().ToListAsync();
        }

        /// <summary>
        /// Insert single row into a specific table
        /// </summary>
        /// <typeparam name="T"> Table that you'd like to insert into </typeparam>
        /// <param name="Data"> Data to insert </param>
        /// <returns>
        /// a boolean, integer tuple. The boolean represents if the transaction was successful and
        /// the integer returns the integer autoincrement primary key if any.
        /// </returns>
        public async Task<bool> InsertDataAsync<T>(T Data)
        {
            return await SqlDataStore.SharedInstance.Database.InsertAsync(Data, typeof(T)) != 0;
        }

        /// <summary>
        /// Insert single row into a specific table
        /// </summary>
        /// <typeparam name="T"> Table that you'd like to insert/replace into </typeparam>
        /// <param name="Data"> Data to insert </param>
        /// <returns>
        /// a boolean, integer tuple. The boolean represents if the transaction was successful and
        /// the integer returns the integer autoincrement primary key if any.
        /// </returns>
        public async Task<bool> InsertOrReplace<T>(T Data)
        {
            return await SqlDataStore.SharedInstance.Database.InsertOrReplaceAsync(Data, typeof(T)) != 0;
        }

        /// <summary>
        /// Updates the specified object
        /// </summary>
        /// <typeparam name="T"> Table that you'd like to update </typeparam>
        /// <param name="Data"> Data you'd like to update </param>
        /// <returns> a boolean that represents if the update was successful </returns>
        public async Task<bool> UpdateDataAsync<T>(T Data)
        {
            return await SqlDataStore.SharedInstance.Database.UpdateAsync(Data, typeof(T)) != 0;
        }
    }
}

