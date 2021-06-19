using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopBridge_Admin_DataAccess;

namespace ShopBridge_Admin_Services
{
    public interface IDbFactory
    {
        ProductsEntities Init();
    }
}
