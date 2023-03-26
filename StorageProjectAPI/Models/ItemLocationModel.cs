using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StorageProject.Api.Models
{
    public class ItemLocationModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Coordinates { get; set; }
    }
}
