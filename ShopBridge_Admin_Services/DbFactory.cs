using ShopBridge_Admin_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge_Admin_Services
{
    public class DbFactory : IDbFactory
    {
        ProductsEntities dbContext;
        public ProductsEntities Init()
        {
            return dbContext ?? (dbContext = new ProductsEntities());
        }
    }
}
