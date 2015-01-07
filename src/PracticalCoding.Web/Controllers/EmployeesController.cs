using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PracticalCoding.Web.Models;
using PracticalCoding.Web.Models.CacheModel;
using StackExchange.Redis;
using PracticalCoding.Web.Utils.Cache;

namespace PracticalCoding.Web.Controllers
{
    public class EmployeesController : ApiController
    {
        // 取得Redis的connection
        IDatabase _redis = RedisConnHelper.Connection.GetDatabase();

        private EF6DbContext db = new EF6DbContext();

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            RecordMetric("get");

            return db.Employees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            RecordMetric("get");

            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, Employee employee)
        {
            RecordMetric("put");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            RecordMetric("post");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            RecordMetric("delete");

            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }

        private string GetCacheKey(string metricName, DateTime when, string where)
        {
            var cacheKeyTemplate = "{0}:{1}:{2}";
            return string.Format(cacheKeyTemplate, metricName, when.ToString("yyyy-MM-dd"), where);
        }

        private string GetHashKey(string webApiName, string methodName)
        {
            var hashKeyTemplate = "{0}:{1}";
            return string.Format(hashKeyTemplate, webApiName, methodName);
        }

        private void RecordMetric(string methodName)
        {
            // 使用Redis的Hash資料結構來儲存Metrics的資料
            _redis.HashIncrementAsync(
                GetCacheKey("webapi_call_count_by_day", DateTime.UtcNow, "server01"),
                GetHashKey("employees", methodName),
                1, CommandFlags.FireAndForget);
        }
    }
}