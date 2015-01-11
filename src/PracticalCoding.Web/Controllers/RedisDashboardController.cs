using Microsoft.AspNet.SignalR;
using NServiceKit.Redis;
using PracticalCoding.Web.Models;
using PracticalCoding.Web.Models.Dashboard;
using PracticalCoding.Web.Models.SignalRHub;
using PracticalCoding.Web.Repository.Dashboard;
using PracticalCoding.Web.Repository.Dashboard.Impl.EF6;
using PracticalCoding.Web.Repository.Dashboard.Impl.Memory;
using PracticalCoding.Web.Repository.Dashboard.Impl.Redis;
using PracticalCoding.Web.SignalRHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using System.IO;
using CsvHelper;

namespace PracticalCoding.Web.Controllers
{
    public class RedisDashboardController : ApiController
    {
        
        // we use a static class to keep the data
        private IDashboardRepo _repo;
        private IRedisClient _redisClient;

        // Retrive a Hub context         
        Lazy<IHubContext> hub = new Lazy<IHubContext>(
          () => GlobalHost.ConnectionManager.GetHubContext<EventBusHub>()
        );

        public RedisDashboardController()
        {
            try
            {
                _redisClient = new RedisClient("localhost");
                _redisClient.ContainsKey("test");

                _repo = new RedisDashboardRepo(_redisClient);
                InitialCheck(_repo);
            }
            catch (Exception)
            {
                throw new HttpUnhandledException("Could not connect to Redis server Ip:[localhost:6379]");
            }
        }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _redisClient.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitialCheck(IDashboardRepo dashboardRepo)
        {
            if (dashboardRepo.GetAllChartdatas().Count == 0)
            {
                //還沒有初始過資料, 用Server上的資料來初始化
                InitChartdatas(dashboardRepo);
            }

        }

        private void InitChartdatas(IDashboardRepo dashboardRepo)
        {                           
            var chartdataFilePath = HttpContext.Current.Server.MapPath("~/chartdata.csv");
            using (var reader = File.OpenText(chartdataFilePath))
            {
                var csv = new CsvReader(reader);

                while (csv.Read())
                {
                    //Period,TAIEX,MonitoringIndex,LeadingIndex,CoincidentIndex,LaggingIndex
                    var period = csv.GetField<string>("Period");
                    var taiex = csv.GetField<decimal>("TAIEX");
                    var monitoringindex = csv.GetField<decimal>("MonitoringIndex");
                    var leadingindex = csv.GetField<decimal>("LeadingIndex");
                    var coincidentindex = csv.GetField<decimal>("CoincidentIndex");
                    var laggingindex = csv.GetField<decimal>("LaggingIndex");

                    var chartdata = new Chartdata(period, taiex, monitoringindex
                            , leadingindex, coincidentindex, laggingindex);

                    dashboardRepo.CreateChartdata(chartdata);
                }
            } //end of using (var reader..)
        }
    }
}
