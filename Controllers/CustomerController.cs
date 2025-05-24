using MyAPI.EF;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyAPI.Controllers
{
    public class CustomerController : ApiController
    {
        PMS_Sp25_AEntities1 db = new PMS_Sp25_AEntities1();

        [HttpGet]
        [Route("api/customer/all")]
        public HttpResponseMessage GetAll()
        {
            var data = db.Customers.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/customer/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = db.Customers.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("api/customer/create")]
        public HttpResponseMessage Create(Customer c)
        {
            c.CreatedAt = DateTime.Now;
            c.CreatedBy = 50;
            db.Customers.Add(c);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, "Customer Created");
        }

        [HttpPut]
        [Route("api/customer/update/{id}")]
        public HttpResponseMessage Update(int id, Customer updatedCustomer)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");
            }

            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;
            customer.Password = updatedCustomer.Password;
            customer.Address = updatedCustomer.Address;
            customer.UpdatedAt = DateTime.Now;
            customer.UpdatedBy = 99;

            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Customer Updated");
        }

        [HttpDelete]
        [Route("api/customer/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");
            }

            db.Customers.Remove(customer);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Customer Deleted");
        }
    }
}
