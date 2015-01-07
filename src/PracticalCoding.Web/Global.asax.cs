using CsvHelper;
using NServiceKit.Redis;
using PracticalCoding.Web.Models;
using PracticalCoding.Web.Models.Dashboard;
using PracticalCoding.Web.Repository.Dashboard.Impl.Elasticsearch;
using PracticalCoding.Web.Repository.Dashboard.Impl.Memory;
using PracticalCoding.Web.Utils.Cache;
using PracticalCoding.Web.Utils.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PracticalCoding.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            #region Omit breavity
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            #endregion

            //Initial Chartdatas using Entity Framework
            InitChartdatas();
            //Initial Chartdatas using Redis
            InitChartdatas_Redis();
            //Initial Chartdatas using Elasticsearch/Redis
            InitChartdatas_Elasticsearch();
        }

        private void InitChartdatas_Elasticsearch()
        {
            var rdClient = RedisConnHelper.Connection.GetDatabase();
            var esClient = ElasticsearchConnHelper.Connection;
            try
            {
                var IDashboardRepo = new ElasticsearchDashboardRepo(esClient, rdClient);
                if (IDashboardRepo.GetAllChartdatas().Count <= 0)
                {
                    var chartdataFilePath = Server.MapPath("~/chartdata.csv");
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

                            IDashboardRepo.CreateChartdata(chartdata);
                        }
                    } //end of using (var reader..)
                }
            }
            catch (Exception e)
            {
                //do nothing
            }
        }

        private void InitChartdatas_Redis()
        {
            try
            {
                using (var redisClient = new RedisClient("localhost"))
                {
                    var rdChartdatas = redisClient.As<Chartdata>();
                    if (rdChartdatas.GetAll().Count <= 0)
                    {
                        var chartdataFilePath = Server.MapPath("~/chartdata.csv");
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

                                chartdata.Id = (int)rdChartdatas.GetNextSequence();
                                rdChartdatas.Store(chartdata);
                            }
                        } //end of using (var reader..)
                    }
                } //end of using (var db...)
            }
            catch (Exception e)
            {
                //do nothing
            }
        }

        private void InitChartdatas()
        {
            using (var db = new EF6DbContext())
            {
                if (db.Chartdatas.Count() <= 0)
                {
                    var chartdataFilePath = Server.MapPath("~/chartdata.csv");
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

                            db.Chartdatas.Add(chartdata);
                        }
                        db.SaveChanges();
                    } //end of using (var reader..)
                }
            } //end of using (var db...)
        }
    }
}
