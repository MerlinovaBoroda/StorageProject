using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StorageProject.Api.Models
{
    public class QrCodeModel
    {
        public string CoreValue { get; set; } = null!;
    }
}
