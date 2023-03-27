using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StorageProject.Api.Configurations;
using StorageProject.Api.Models;

namespace StorageProject.Api.Services
{
    public class ItemsService
    {
        private readonly IMongoCollection<ItemModel> _itemCollection;

        public ItemsService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);

            _itemCollection = mongoDb.GetCollection<ItemModel>(databaseSettings.Value.ItemsCollection);
        }

        public async Task<List<ItemModel>> GetAsync() => await _itemCollection.Find(_ => true).ToListAsync();

        public async Task<ItemModel> GetAsync(string id) => 
            await _itemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ItemModel item) => await _itemCollection.InsertOneAsync(item);

        public async Task UpdateAsync(ItemModel item) => await _itemCollection.ReplaceOneAsync(x => x.Id == item.Id, item);

        public async Task RemoveAsync(string id) => await _itemCollection.DeleteOneAsync(x => x.Id == id);


        public async Task<ItemModel> GetSerialAsync(string serialNumber) =>
            await _itemCollection.Find(x => x.SerialNumber == serialNumber).FirstOrDefaultAsync();
    }
}
