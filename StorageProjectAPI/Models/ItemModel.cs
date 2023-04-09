using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StorageProject.Api.Models
{
    public class ItemModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ItemTypeId { get; set; } = null!;

        [BsonIgnore]
        public ItemTypeModel? ItemType { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string? Description { get; set; }
        public string? Logo { get; set; }
        public DateTime Warranty { get; set; }
        public string ProviderId { get; set; } = null!;

        [BsonIgnore]
        public ProviderModel? Provider { get; set; }
        
        public QrCodeModel? QrCode { get; set; }
        
        public ItemLocationModel? Location { get; set; }
        

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}