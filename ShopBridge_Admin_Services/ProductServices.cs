using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopBridge_Admin_DataAccess;
using System.Data.Entity;

namespace ShopBridge_Admin_Services
{
    public class ProductServices : IProduct
    {
        private ProductsEntities dbContext;

        protected IDbFactory DbFactory { get; private set; }

        protected ProductsEntities DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }

        public ProductServices(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        public async Task DeleteProduct(int id)
        {
            using (ProductsEntities db = new ProductsEntities())
            {
                var prod = await db.Product.FirstOrDefaultAsync(p=>p.ID == id).ConfigureAwait(false);
                db.Product.Remove(prod);
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using (ProductsEntities db = new ProductsEntities())
            {
                var prod = await db.Product.ToListAsync().ConfigureAwait(false);
                return prod;
            }
        }

        public async Task<List<Product>> GetProductByCategory(string categoryName)
        {
            using (ProductsEntities db = new ProductsEntities())
            {
                List<Product> categorisedProducts = await db.Product.Where(prod=>prod.CATEGORY.ToUpper() == categoryName.ToUpper())
                    .ToListAsync().ConfigureAwait(false);
                return categorisedProducts;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            using (ProductsEntities db = new ProductsEntities())
            {
                var prod = await db.Product.FirstOrDefaultAsync(p => p.ID == id).ConfigureAwait(false);
                return prod;
            }
        }

        public async Task SaveProduct(Product item)
        {
            using (ProductsEntities db = new ProductsEntities())
            {
                db.Product.Add(item);
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateProduct(int id, Product item)
        {
            using (ProductsEntities db = new ProductsEntities())
            {
                var prod = db.Product.FirstOrDefault(p => p.ID == id);

                prod.IMAGEURL = item.IMAGEURL;
                prod.PRICE = item.PRICE;
                prod.TITLE = item.TITLE;
                prod.AVAILABLEUNITS = item.AVAILABLEUNITS;
                prod.CATEGORY = item.CATEGORY;
                prod.DESCRIPTION = item.DESCRIPTION;

                await db.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
