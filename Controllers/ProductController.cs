using MyAPI.EF;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyAPI.Controllers
{
    public class ProductController : ApiController
    {
        PMS_Sp25_AEntities1 db = new PMS_Sp25_AEntities1();

        [HttpGet]
        [Route("api/product/all")]
        public HttpResponseMessage GetAll()
        {
            var data = db.Products.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/product/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = db.Products.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("api/product/create")]
        public HttpResponseMessage Create(Product p)
        {
            p.CreatedAt = DateTime.Now;
            p.CreatedBy = 99;
            db.Products.Add(p);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, "Product Created");
        }

        [HttpPut]
        [Route("api/product/update/{id}")]
        public HttpResponseMessage Update(int id, Product updatedProduct)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found");
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Qty = updatedProduct.Qty;
            product.CId = updatedProduct.CId;
            // product.CreatedAt and product.CreatedBy are not updated here

            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Product Updated");
        }

        [HttpDelete]
        [Route("api/product/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Product not found");
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Product Deleted");
        }
    }
}
