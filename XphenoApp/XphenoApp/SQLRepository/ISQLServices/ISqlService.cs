using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace XphenoApp.SQLRepository.ISQLServices
{
    public interface ISqlService
    {
        Task<bool> InsertDataAsync<T>(T Data);

        Task<bool> DeleteDataAsync<T>(object primaryKey);

        Task<bool> UpdateDataAsync<T>(T Data);

        Task<bool> InsertOrReplace<T>(T Data);

        Task<List<T>> GetAllDataAsync<T>() where T : new();
    }
}

