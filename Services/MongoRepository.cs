using MongoDB.Driver;

using SampelToDo.Models;
using SampelToDo.Services.Interfaces;

namespace SampelToDo.Services
{
    public class MongoRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(MongoUrl mongoUrl)
        {
            var dbClient = new MongoClient(mongoUrl);
            var db = dbClient.GetDatabase(mongoUrl.DatabaseName);
            _collection = db.GetCollection<T>($"{typeof(T).Name.ToLower()}s");
        }
        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
          return await _collection.Find(_=>true).ToListAsync();
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _collection.Find(x=>x.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(e=>e.Id.Equals(entity.Id),entity);
        }
    }
}
