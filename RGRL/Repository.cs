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
            var type = typeof(T);
            var properties = type.GetProperties();

            var sql = $"INSERT INTO {type.Name} ({GetParams(properties)}) VALUES ({GetParams(properties, includeAt:true)})";

            return await _sqlConnection.ExecuteAsync(sql, obj);
        }

        private static string GetParams(IEnumerable<PropertyInfo> propertyInfos, bool includeAt = false)
        {
            return string.Join(',', propertyInfos.Select(p => (includeAt ? "@" : "") + p.Name));
        }
    }
}
