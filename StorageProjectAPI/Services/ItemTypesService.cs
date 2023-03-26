using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StorageProject.Api.Configurations;
using StorageProject.Api.Models;

namespace StorageProject.Api.Services
{
    public class ItemTypesService
    {
        private readonly IMongoCollection<ItemTypeModel> _itemTypeCollection;

        public ItemTypesService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _itemTypeCollection = mongoDb.GetCollection<ItemTypeModel>(databaseSettings.Value.ItemTypesCollection);
        }

        public async Task<List<ItemTypeModel>> GetAsync() => await _itemTypeCollection.Find(_ => true).ToListAsync();
        public async Task<ItemTypeModel> GetAsync(string name) => 
            await _itemTypeCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
        public async Task CreateAsync(ItemTypeModel itemType) => await _itemTypeCollection.InsertOneAsync(itemType);

    }
}
