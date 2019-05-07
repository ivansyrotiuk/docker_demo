using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarRentalService.Controllers
{
    public class Order
    {
        [BsonId]
        [BsonElement("EntityId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int CustomerId { get; set; }

        public int CarId { get; set; }
    }
}