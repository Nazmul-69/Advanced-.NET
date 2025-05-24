using MyAPI.EF;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        PMS_Sp25_AEntities1 db = new PMS_Sp25_AEntities1();

        [HttpGet]
        [Route("api/employee/all")]
        public HttpResponseMessage GetAll()
        {
            var data = db.Employees.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("api/employee/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var data = db.Employees.Find(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [Route("api/employee/create")]
        public HttpResponseMessage Create(Employee e)
        {
            e.CreatedAt = DateTime.Now;
            e.CreatedBy = 99;
            db.Employees.Add(e);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Created, "Employee Created");
        }

        [HttpPut]
        [Route("api/employee/update/{id}")]
        public HttpResponseMessage Update(int id, Employee updatedEmployee)
        {
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");
            }

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.Address = updatedEmployee.Address;
            employee.Password = updatedEmployee.Password;
            employee.Type = updatedEmployee.Type;
            employee.UpdatedAt = DateTime.Now;
            employee.UpdatedBy = 99;

            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Employee Updated");
        }

        [HttpDelete]
        [Route("api/employee/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Employee not found");
            }

            db.Employees.Remove(employee);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Employee Deleted");
        }
    }
}
