using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StorageProject.Api.Configurations;
using StorageProject.Api.Models;

namespace StorageProject.Api.Services
{
    public class ProvidersService
    {
        private readonly IMongoCollection<ProviderModel> _providerCollection;

        public ProvidersService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _providerCollection = mongoDb.GetCollection<ProviderModel>(databaseSettings.Value.ProvidersCollection);
        }

        public async Task<List<ProviderModel>> GetAsync() => await _providerCollection.Find(_ => true).ToListAsync();

        public async Task<ProviderModel> GetAsync(string name) =>
            await _providerCollection.Find(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();

        public async Task<ProviderModel> GetAsync(int code) =>
            await _providerCollection.Find(x => x.EdrpouCode == code).FirstOrDefaultAsync();

        public async Task<ProviderModel> GetAsyncById(string id) =>
            await _providerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ProviderModel itemType) => await _providerCollection.InsertOneAsync(itemType);

        public async Task RemoveAsync(string id) => await _providerCollection.DeleteOneAsync(x => x.Id == id);
    }
}
