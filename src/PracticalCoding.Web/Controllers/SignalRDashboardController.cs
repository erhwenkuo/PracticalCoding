using Microsoft.AspNet.SignalR;
using PracticalCoding.Web.Models.Dashboard;
using PracticalCoding.Web.Models.SignalRHub;
using PracticalCoding.Web.Repository.Dashboard;
using PracticalCoding.Web.Repository.Dashboard.Impl.Memory;
using PracticalCoding.Web.SignalRHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PracticalCoding.Web.Controllers
{
    public class SignalRDashboardController : ApiController
    {
        // Get a data repository from Utitity class. To simplify the demonstration
        // we use a static class to keep the data
        IDashboardRepo _repo = MemDashboardRepoUtil.GetDashboardRepo();

        // Retrive a Hub context         
        Lazy<IHubContext> hub = new Lazy<IHubContext>(
          () => GlobalHost.ConnectionManager.GetHubContext<EventBusHub>()
        );

        protected IHubContext Hub
        {
            get { return hub.Value; }
        }

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
            var response = Request.CreateResponse<int>
                (HttpStatusCode.Created, newEntityId);
            string uri = Url.Link("DefaultApi", new { id = newEntityId });
            response.Headers.Location = new Uri(uri);

            // *** Broadcast to all Signalr client ***
            value.Id = newEntityId;
            var signalrEvent = new SignalREvent{
                EventName="chartdata_created" , 
                EventBody=new {Data=value}, 
                EventDate=DateTime.Now, 
                EventSource="[POST]@/api/dashboard"};
            Task.Factory.StartNew(() => 
                { Hub.Clients.All.broadcastSignalrEvent(signalrEvent); });

            return response;
        }

        // PUT: api/Dashboard/5 (Update)
        public void Put(int id, [FromBody]Chartdata value)
        {
            var originEntity = _repo.GetChartdataById(id);
            if (originEntity == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
            {
                _repo.UpdateChartdata(value);

                // *** Broadcast to all Signalr client ***
                var signalrEvent = new SignalREvent { 
                    EventName = "chartdata_updated", 
                    EventBody = new { Data = value }, 
                    EventDate = DateTime.Now, 
                    EventSource = "[PUT]@/api/dashboard/{id}" };
                Task.Factory.StartNew(() => 
                    { Hub.Clients.All.broadcastSignalrEvent(signalrEvent); });
            }
        }

        // DELETE: api/Dashboard/5
        public void Delete(int id)
        {
            var originEntity = _repo.GetChartdataById(id);
            if (originEntity == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
            {
                _repo.DeleteChartdataById(id);

                // *** Broadcast to all Signalr client ***
                var signalrEvent = new SignalREvent { EventName = "chartdata_deleted", 
                    EventBody = new { Data = id }, EventDate = DateTime.Now, 
                    EventSource = "[DELETE]@/api/dashboard/{id}" };
                Task.Factory.StartNew(() => 
                { Hub.Clients.All.broadcastSignalrEvent(signalrEvent); });                
            }
        }
    }
}
