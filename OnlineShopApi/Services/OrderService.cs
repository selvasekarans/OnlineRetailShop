using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OnlineShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static OnlineShopApi.Models.OnlineShopDbSettings;

namespace OnlineShopApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _Orders;
        private readonly IServiceProvider _provider;

        public OrderService(IServiceProvider provider, IOnlineShopDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Orders = database.GetCollection<Order>(settings.OrdersCollectionName);
            _provider = provider;
        }

        public List<Order> Get() =>
            _Orders.Find(Order => true).ToList();

        public Order Get(string id) =>
            _Orders.Find<Order>(Order => Order.Id == id).FirstOrDefault();

        public Order Create(Order Order)
        {
            _provider.GetRequiredService<IProductService>().UpdateRemainingQuantity(Order.ProductId, Order.Quantity);
            _Orders.InsertOne(Order);
            return Order;
        }

        public void Update(string id, Order OrderIn)
        {
            _provider.GetRequiredService<IProductService>().UpdateRemainingQuantity(OrderIn.ProductId, OrderIn.Quantity);
            _Orders.ReplaceOne(Order => Order.Id == id, OrderIn);
        }
    }
    public interface IOrderService
    {
        List<Order> Get();
        Order Get(string id);
        Order Create(Order Order);
        void Update(string id, Order OrderIn);
    }

    [Serializable]
    public class RetailShopException : Exception
    {
        public RetailShopException() { }

        public RetailShopException(string name)
            : base(String.Format("Exception: {0}", name))
        {

        }

        protected RetailShopException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }
}
