using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StorageProject.Api.Models
{
    public class ProviderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;

        //TODO: додати валідацію
        public int EdrpouCode { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string ProviderType { get; set; } = null!;
        public string? Logo { get; set; }
    }
}
