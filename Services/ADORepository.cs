using EFCore.NamingConventions.Internal;

using SampelToDo.Models;
using SampelToDo.Services.Interfaces;

using System.Data;
using System.Data.Common;
using System.Security.Cryptography;

using static Npgsql.PostgresTypes.PostgresCompositeType;

namespace SampelToDo.Services
{
    public class ADORepository<T> : IRepository<T> where T : BaseModel
    {

        private readonly DbConnection _dbConnection;
        private readonly INameRewriter _nameRewriter;
        private string nameT = $"{typeof(T).Name.ToLower()}s";

        public ADORepository(DbConnection dbConnection, INameRewriter nameRewriter)
        {
            
            _dbConnection = dbConnection;
            _nameRewriter = nameRewriter;
            if (dbConnection.State != ConnectionState.Open) _dbConnection.Open();
           
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            _dbConnection.ConnectionString = "Provider=PostgreSQL OLE DB Provider;Data Source=127.0.0.1:5432;location=TodoDb_test;User ID=postgres;password=tr134sdfWE;timeout=1000;";
            List<T> result = new List<T>();
            await using (var cmd = _dbConnection.CreateCommand())
            {
                cmd.CommandText = $"SELECT *FROM {nameT}";
                await using (var reader =await cmd.ExecuteReaderAsync())
                {
                    while(reader.Read())
                    {
                        var item = Activator.CreateInstance<T>();
                        foreach (var data in item.GetType().GetProperties())
                        {
                            data.SetValue(item, reader[_nameRewriter.RewriteName(data.Name)]);
                        }
                        result.Add(item);
                    }
                }
            }
            return result;
        }
        public async Task<T?> GetAsync(Guid id)
        {
            T result=Activator.CreateInstance<T>();

            await using (var cmd = _dbConnection.CreateCommand())
            {
                cmd.CommandText = $"SELECT * FROM {nameT} WHERE Id='{id}'";
                await using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                       
                        foreach (var data in result.GetType().GetProperties())
                        {
                            data.SetValue(result, reader[_nameRewriter.RewriteName(data.Name)]);
                        }
                       
                    }
                }
            }
            return result;
        }

        public async Task AddAsync(T entity)
        {
            await using (var cmd = _dbConnection.CreateCommand())
            {
                string fields = "";
                string value = "";
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (!string.IsNullOrEmpty(fields))
                    {
                        value += ",";
                        fields += ",";
                    }
                    fields += _nameRewriter.RewriteName(item.Name);
                    value += $"'{item.GetValue(entity)}'";
                }
                cmd.CommandText=$"INSERT INTO {nameT} ({fields}) VALUES ({value})";
                await cmd.ExecuteNonQueryAsync();

            }
        }

        public async Task UpdateAsync(T entity)
        {
            await using (var cmd = _dbConnection.CreateCommand())
            {
                string values = "";
                foreach (var item in entity.GetType().GetProperties().Where(x=>x.Name!="Id"))
                {
                    if (!string.IsNullOrEmpty(values))
                    {
                        values += ",";
                       
                    }
                  
                    values += $"{_nameRewriter.RewriteName(item.Name)} = '{item.GetValue(entity)}'";
                }
                cmd.CommandText = $"UPDATE {nameT} SET {values} WHERE Id='{entity.Id}'";
                await cmd.ExecuteNonQueryAsync();

            }
        }
        public async Task DeleteAsync(Guid id)
        {
            await using (var cmd = _dbConnection.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM {nameT} WHERE Id='{id}'";
                await cmd.ExecuteNonQueryAsync();
            }
        }


    }
}
