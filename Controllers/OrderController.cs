using MyAPI.EF;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyAPI.Controllers
{
    public class OrderController : ApiController
    {
        PMS_Sp25_AEntities1 db = new PMS_Sp25_AEntities1();

        // Get all orders
        [HttpGet]
        [Route("api/order/all")]
        public HttpResponseMessage GetAll()
        {
            var data = db.Orders.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        // Get order by id
        [HttpGet]
        [Route("api/order/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = db.Orders.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
