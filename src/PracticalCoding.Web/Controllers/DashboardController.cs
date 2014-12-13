using PracticalCoding.Web.Models.Dashboard;
using PracticalCoding.Web.Repository.Dashboard;
using PracticalCoding.Web.Repository.Dashboard.Impl.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PracticalCoding.Web.Controllers
{
    public class DashboardController : ApiController
    {
        // Get a data repository from Utitity class. To simplify the demonstration
        // we use a static class to keep the data
        IDashboardRepo _repo = MemDashboardRepoUtil.GetDashboardRepo();

        // GET: api/Dashboard (Query)
        public IEnumerable<Chartdata> Get()
        {
            return _repo.GetAllChartdatas();
        }

        // GET: api/Dashboard/5 (Query)
        public IHttpActionResult Get(int id)
        {
            var entity = _repo.GetChartdataById(id);
            if (entity == null)
                return NotFound();
            else 
                return Ok(entity);
        }

        // POST: api/Dashboard (Create)
        public HttpResponseMessage Post([FromBody]Chartdata value)
        {
            var newEntityId = _repo.CreateChartdata(value);
            var response = Request.CreateResponse<int>(HttpStatusCode.Created, newEntityId);
            string uri = Url.Link("DefaultApi", new { id = newEntityId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        // PUT: api/Dashboard/5 (Update)
        public void Put(int id, [FromBody]Chartdata value)
        {
            var originEntity = _repo.GetChartdataById(id);
            if (originEntity == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                _repo.UpdateChartdata(value);

        }

        // DELETE: api/Dashboard/5
        public void Delete(int id)
        {
            var originEntity = _repo.GetChartdataById(id);
            if (originEntity == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
                _repo.DeleteChartdataById(id);

        }
    }
}
