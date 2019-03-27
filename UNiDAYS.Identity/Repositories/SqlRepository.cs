using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UNiDAYS.Identity.Repositories
{
    public class SqlRepository<TData> : IRepository<TData> where TData : class
    {
        private readonly string _connectionString;
        private readonly ISqlTranslator<TData> _sqlTranslator;
        private readonly IMethodLookup _sqlMethodLookup;

        public SqlRepository(IConfiguration config, ISqlTranslator<TData> sqlTranslator, IMethodLookup methodLookup)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
            _sqlTranslator = sqlTranslator;
            _sqlMethodLookup = methodLookup;
        }

        public async Task Execute(string methodName, CancellationToken token, params (string name, object value)[] paramters)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = _sqlMethodLookup.StoredProcedure(methodName);
                if (paramters != null)
                    sqlCommand.Parameters.AddRange(paramters.Select(x => new SqlParameter(x.name, x.value)).ToArray());
                await sqlConnection.OpenAsync(token);
                if (token.IsCancellationRequested)
                    return;
                await sqlCommand.ExecuteNonQueryAsync(token);
            }
        }

        public async Task<string> GetString(string methodName, CancellationToken token, params (string name, object value)[] paramters)
        {
            string result = null;
            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = _sqlMethodLookup.StoredProcedure(methodName);
                if (paramters != null)
                    sqlCommand.Parameters.AddRange(paramters.Select(x => new SqlParameter(x.name, x.value)).ToArray());

                await sqlConnection.OpenAsync(token);
                if (token.IsCancellationRequested)
                    return result;
                var ret = await sqlCommand.ExecuteScalarAsync(token);
                if (ret != null)
                    result = Convert.ToString(ret);
            }
            return result;
        }

        public async Task<TData> GetData(string methodName, CancellationToken token, params (string name, object value)[] paramters)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = _sqlMethodLookup.StoredProcedure(methodName);
                if (paramters != null)
                    sqlCommand.Parameters.AddRange(paramters.Select(x => new SqlParameter(x.name, x.value)).ToArray());
                try
                {
                    await sqlConnection.OpenAsync(token);
                    if (token.IsCancellationRequested)
                        return default(TData);
                    using (var reader = await sqlCommand.ExecuteReaderAsync(token))
                    {
                        TData elements = default(TData);
                        try
                        {
                            elements = _sqlTranslator.Translate(reader);
                        }
                        finally
                        {
                            while (reader.NextResult())
                            { }
                        }
                        return elements;
                    }
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
        }

        public async Task<List<TData>> GetDataList(string methodName, CancellationToken token, params (string name, object value)[] paramters)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            using (var sqlCommand = sqlConnection.CreateCommand())
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = _sqlMethodLookup.StoredProcedure(methodName);
                if (paramters != null)
                    sqlCommand.Parameters.AddRange(paramters.Select(x => new SqlParameter(x.name, x.value)).ToArray());
                await sqlConnection.OpenAsync(token);
                if (token.IsCancellationRequested)
                    return new List<TData>();
                using (var reader = await sqlCommand.ExecuteReaderAsync(token))
                {
                    List<TData> elements = new List<TData>();
                    try
                    {
                        elements = _sqlTranslator.TranslateAsList(reader);
                    }
                    finally
                    {
                        while (reader.NextResult())
                        { }
                    }
                    return elements;
                }
            }
        }
    }
}
