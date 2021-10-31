using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopApi.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("OrderId")]
        public int OrderId { get; set; }

        [BsonElement("ProductId")]
        public int ProductId { get; set; }
        public string OrderBy { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
    }
}
