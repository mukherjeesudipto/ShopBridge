using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ShopBridge_Admin_DataAccess;
using ShopBridge_Admin_Services;
using System.Data.Entity;
using System.IdentityModel;
using WebApi.OutputCache.V2;

namespace ShopBridge_Admin_WebAPI.Controllers
{ 
    public class ProductController : ApiController
    {
        private readonly IProduct _prodService;

        Dictionary<string, IEnumerable<Product>> dictProduct = new Dictionary<string, IEnumerable<Product>>();

        public ProductController(IProduct prodservice)
        {
            this._prodService = prodservice;
        }

        public ProductController() : base() { }

        [HttpGet]
        [CacheOutput(ServerTimeSpan = 5)]
        public IHttpActionResult FetchAllProducts()
        {
            var productList = _prodService.GetAllProducts();
            dictProduct.Add("products", productList.Result);

            return Ok(dictProduct);
        }

        [HttpGet]
        [CacheOutput(ServerTimeSpan = 5)]
        public IHttpActionResult FetchProductById([FromUri] int id)
        {
            var product = _prodService.GetProductById(id);

            if (product.Result != null)
            {
                return Ok(product);
            }

            return Content(HttpStatusCode.NotFound, string.Format("Product with Id {0} not found", id));
        }

        [HttpGet]
        [CacheOutput(ServerTimeSpan = 5)]
        public IHttpActionResult FetchProductByCategory([FromUri] string category)
        {
            var categorisedProducts = _prodService.GetProductByCategory(category);

            if(categorisedProducts.Result.Count != 0)
            {
                return Ok(categorisedProducts);
            }

            return Content(HttpStatusCode.NotFound, string.Format("Products with requested category {0} not found.", category));
        }

        [HttpPost]
        public IHttpActionResult AddProduct([FromBody] Product prod)
        {
            try
            {
                if (prod != null)
                {
                    _prodService.SaveProduct(prod);

                    return Content(HttpStatusCode.OK, "Product added successfully");
                }
                else
                {
                    throw new BadRequestException();
                }

                
            }
            catch(BadRequestException bax)
            {
                return Content(HttpStatusCode.BadRequest, "Bad Request. Please check the field values.");
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadGateway, ex.ToString());
            }

        }

        [HttpPut]
        public IHttpActionResult ModifyProduct([FromUri] int id, [FromBody] Product prod)
        {
            try
            {
                var product = _prodService.GetProductById(id);

                if (product.Result != null)
                {
                    _prodService.UpdateProduct(id, prod);
                    return Content(HttpStatusCode.OK, string.Format("Product with id {0} updated successfully.", id));
                }

                return Content(HttpStatusCode.NotFound, string.Format("Product with id {0} does not exist.", id));
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadGateway, ex.ToString());
            }
        }

        [HttpDelete]
        public IHttpActionResult RemoveProduct([FromUri] int id)
        {
            try
            {
                var product = _prodService.GetProductById(id);

                if (product.Result != null)
                {
                    _prodService.DeleteProduct(id);
                    return Content(HttpStatusCode.OK, string.Format("Product with id {0} deleted successfully.", id));
                }

                return Content(HttpStatusCode.NotFound, string.Format("Product with id {0} does not exist.", id));
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadGateway, ex.ToString());
            }
        }
    }
}
