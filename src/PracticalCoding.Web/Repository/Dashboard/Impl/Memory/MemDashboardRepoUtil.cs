using CsvHelper;
using PracticalCoding.Web.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Repository.Dashboard.Impl.Memory
{
    public class MemDashboardRepoUtil
    {
        private static MemDashboardRepo _memDashboardRepo;
        static MemDashboardRepoUtil()
        {
            _memDashboardRepo = new MemDashboardRepo();
            InitRepoData();
        }
        private static void InitRepoData(){
            var chartdataFilePath = HttpContext.Current.Request.MapPath("~/chartdata.csv");
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

                    // Use repository to add Chartdata objects
                    _memDashboardRepo.CreateChartdata(
                        new Chartdata(period, taiex, monitoringindex
                            , leadingindex, coincidentindex, laggingindex));
                }
            }
        }
        public static IDashboardRepo GetDashboardRepo()
        {
            return _memDashboardRepo;
        }
    }
}