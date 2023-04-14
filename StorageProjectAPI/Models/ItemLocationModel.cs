using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StorageProject.Api.Models
{
    public class ItemLocationModel
    {
        public string? LocationName { get; set; }
        public string? LocationFloor { get; set; }
        public int? CoordinateX { get; set; }
        public int? CoordinateY { get; set; }
    }
}
