using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StorageProject.Api.Models
{
    public class ItemsModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public int Amount { get; set; }
        public double PriceForOne { get; set; }
        public string TypeOfMeasurement { get; set; } = null!;
    }
}