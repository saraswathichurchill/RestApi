using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

// Sample CRUD operation RestApi using Entity Framework Designer from Database Model Approach

// API URL : http//localhost:port/api/Employee

namespace RestAPIWA.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        // GET api/Employee used to get all the Employee details from the table
        /* Sample JSON response String
         [
            {
                "Id": 1,
                "Name": "sample string 2",
                "Age": 1,
                "isActive": true
            },
            {
                "Id": 1,
                "Name": "sample string 2",
                "Age": 1,
                "isActive": true
            }
        ]
        */

        [HttpGet]
        [Route("")]
        public List<Employee> GetAllEmployee()
        {

            using (mytestdbEntities entities = new mytestdbEntities())
            {
                return entities.Employees.ToList();
            }
        }
        // GET api/Employee/1 used to get specific Employee details from the table
        /* Sample JSON response String
            {
            "Id": 1,
            "Name": "sample string 2",
            "Age": 1,
            "isActive": true
            }
        */
        [HttpGet]
        [Route("{id:int}")]
        // GET: api/Employee/5
        public Employee Get(int id)
        {
            using(mytestdbEntities entities=new mytestdbEntities())
            {
                return entities.Employees.FirstOrDefault(e => e.Id == id);
            }
           
        }
        // POST Method api/Employee/ used to add new Employee details into the table
        /* Sample JSON request String
            {
            "Id": 1,
            "Name": "sample string 2",
            "Age": 1,
            "isActive": true
            }
        */
        // POST: api/Employee
        [HttpPost]
        [Route("")]
        public void Post(Employee emp)
        {
            using(mytestdbEntities entities= new mytestdbEntities())
            {
                Employee em = new Employee();
                em.Id = emp.Id;
                em.Name = emp.Name;
                em.Age = Convert.ToInt32(emp.Age);
                em.isActive = Convert.ToBoolean(emp.isActive);
                entities.Employees.Add(em);
                entities.SaveChanges();
            }
            
        }
        // PUT Method api/Employee/ used to update the existing Employee details from the table
        /* Sample JSON request String
            {
            "Id": 1,
            "Name": "sample string 2",
            "Age": 1,
            "isActive": true
            }
        */
        // PUT: api/Employee/5
        [HttpPut]
        [Route("")]
        public IHttpActionResult Put(Employee emp)
        {
            using (mytestdbEntities entities = new mytestdbEntities())
            {
                var existingemployee = entities.Employees.Where(s => s.Id == emp.Id).FirstOrDefault<Employee>();
                if (existingemployee != null)
                {
                    existingemployee.Name = emp.Name;
                    existingemployee.Age = Convert.ToInt32(emp.Age);
                    existingemployee.isActive = Convert.ToBoolean(emp.isActive);
                    entities.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        // DELETE Method api/Employee/1 used to delete the Employee details from the table
        /* Sample JSON request String
            {
            "Id": 1,
            "Name": "sample string 2",
            "Age": 1,
            "isActive": true
            }
        */
        [HttpDelete]
        [Route("{id:int}")]
        // DELETE: api/Employee/5
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Not a valid id");
            }
            using (mytestdbEntities entities = new mytestdbEntities())
            {
                var existingemployee = entities.Employees.Where(s => s.Id == id).FirstOrDefault<Employee>();
                if (existingemployee != null)
                {
                    entities.Entry(existingemployee).State = System.Data.Entity.EntityState.Deleted;
                    entities.SaveChanges();
                }
                else
                {
                    return BadRequest("Id not exists");
                }

            }
            return Ok();

        }
    }
}