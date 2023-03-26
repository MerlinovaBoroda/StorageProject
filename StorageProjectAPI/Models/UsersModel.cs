using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StorageProject.Api.Models
{
    public class UsersModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Description { get; set; }
        public AccessLevelModel? AccessLevel { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
