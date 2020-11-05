using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;

namespace RGRL
{
    public class Repository<T> : IRepository<T>
    {
        private readonly SqlConnection _sqlConnection;

        public Repository(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }

        public async Task<int> Create(T obj)
        {
            var t = typeof(T);
            var sql = $"INSERT INTO [{t.Name}] ({GetParams(t)}) VALUES ({GetParams(t, includeAt:true)})";

            return await _sqlConnection.ExecuteAsync(sql, obj);
        }

        private static string GetParams(Type t, bool includeAt = false)
        {
            var properties = t.GetProperties();
            return string.Join(',', properties.Select(p => (includeAt ? "@" : "") + p.Name));
        }
    }
}
