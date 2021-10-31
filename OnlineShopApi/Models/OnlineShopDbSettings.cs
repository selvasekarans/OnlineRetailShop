using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopApi.Models
{
    public class OnlineShopDbSettings
    {
        public class OnlineShopDatabaseSettings : IOnlineShopDbSettings
        {
            public string ProductsCollectionName { get; set; }
            public string OrdersCollectionName { get; set; }
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

        public interface IOnlineShopDbSettings
        {
            string ProductsCollectionName { get; set; }
            string OrdersCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    }
}
