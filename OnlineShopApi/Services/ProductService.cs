using MongoDB.Driver;
using OnlineShopApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineShopApi.Models.OnlineShopDbSettings;

namespace OnlineShopApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _Products;

        public ProductService(IOnlineShopDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Products = database.GetCollection<Product>(settings.ProductsCollectionName);
        }

        public List<Product> Get() =>
            _Products.Find(Product => true).ToList();

        public Product Get(string id) =>
            _Products.Find(Product => Product.Id == id).FirstOrDefault();

        public Product Create(Product Product)
        {
            _Products.InsertOne(Product);
            return Product;
        }

        public void Update(string id, Product ProductIn)
        {
            _Products.ReplaceOne(Product => Product.Id == id, ProductIn);
        }

        public void Remove(Product ProductIn)
        {
            _Products.DeleteOne(Product => Product.Id == ProductIn.Id);
        }

        public void Remove(string id)
        {
            _Products.DeleteOne(Product => Product.Id == id);
        }

        public int UpdateRemainingQuantity(int productId, int quantity)
        {
            var product = _Products.Find(Product => Product.ProductId == productId).FirstOrDefault();
            int remaining = product.Quantity - quantity;
            if (remaining < 0)
                throw new RetailShopException("Unavailability of the product");
            product.Quantity = remaining;
            _Products.ReplaceOne(Product => Product.Id == product.Id, product);
            return remaining;
        }
    }

    public interface IProductService
    {
        List<Product> Get();
        Product Get(string id);
        Product Create(Product Product);
        void Update(string id, Product ProductIn);
        void Remove(Product ProductIn);
        void Remove(string id);
        int UpdateRemainingQuantity(int productId, int quantity);
    }
}
