using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StorageProject.Api.Configurations;
using StorageProject.Api.Models;

namespace StorageProject.Api.Services
{
    public class ItemsService
    {
        private readonly IMongoCollection<ItemsModel> _mongoCollection;

        public ItemsService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _mongoCollection = mongoDb.GetCollection<ItemsModel>(databaseSettings.Value.ItemsCollection);
        }

        public async Task<List<ItemsModel>> GetAsync() => await _mongoCollection.Find(_ => true).ToListAsync();

        public async Task<ItemsModel> GetAsync(string id) => 
            await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ItemsModel item) => await _mongoCollection.InsertOneAsync(item);

        public async Task UpdateAsync(ItemsModel item) => await _mongoCollection.ReplaceOneAsync(x => x.Id == item.Id, item);

        public async Task RemoveAsync(string id) => await _mongoCollection.DeleteOneAsync(x => x.Id == id);
    }
}
