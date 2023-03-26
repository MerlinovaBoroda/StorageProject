using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StorageProject.Api.Models
{
    public class ItemModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public ItemTypeModel ItemType { get; set; } = null!;
        public string Manufacturer { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public DateTime Warranty { get; set; }
        public ProviderModel Provider { get; set; } = null!;
        public QrCodeModel? QrCode { get; set; }
        public ItemLocationModel? Location { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}