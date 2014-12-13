using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Models.Dashboard
{
    public class Chartdata
    {
        public Chartdata(string period, decimal taiex, decimal monitoringindex
            , decimal leadingindex, decimal coincidentindex, decimal laggingindex)
        {
            this.Period = period;
            this.Taiex = taiex;
            this.MonitoringIndex = monitoringindex;
            this.LeadingIndex = leadingindex;
            this.CoincidentIndex = coincidentindex;
            this.LaggingIndex = laggingindex;
            this.Period_UTC = parseDateToUTC(period); //convert string to UTC millis
        }
        public int Id { get; set; }

        public string Period { get; set; }

        public double Period_UTC { get; set; }

        public decimal Taiex { get; set; }

        public decimal MonitoringIndex { get; set; }

        public decimal LeadingIndex { get; set; }

        public decimal CoincidentIndex { get; set; }

        public decimal LaggingIndex {get;set;}

        private double parseDateToUTC(string period){
            var splits = period.Split('/');
            var year = Convert.ToInt32(splits[0]);
            var month = Convert.ToInt32(splits[1]);
            var day = Convert.ToInt32(splits[2]);
            var tmpDate = new DateTime(year, month, day);
            return (tmpDate.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}


