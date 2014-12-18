using PracticalCoding.Web.Models;
using PracticalCoding.Web.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Repository.Dashboard.Impl.EF6
{
    public class EF6DashboardRepo : IDashboardRepo
    {
        private EF6DbContext _dbCtx;

        public EF6DashboardRepo(EF6DbContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public int CreateChartdata(Models.Dashboard.Chartdata entity)
        {
            entity.Period_UTC = entity.parseDateToUTC(entity.Period);
            _dbCtx.Chartdatas.Add(entity);
            _dbCtx.SaveChanges();
            return entity.Id;
        }

        public void UpdateChartdata(Models.Dashboard.Chartdata entity)
        {
            var oriEntity = _dbCtx.Chartdatas.Find(entity.Id);
            if (oriEntity != null)
            {
                oriEntity.Period = entity.Period;
                oriEntity.Period_UTC = oriEntity.parseDateToUTC(entity.Period);
                oriEntity.Taiex = entity.Taiex;
                oriEntity.MonitoringIndex = entity.MonitoringIndex;
                oriEntity.LeadingIndex = entity.LeadingIndex;
                oriEntity.CoincidentIndex = entity.CoincidentIndex;
                oriEntity.LaggingIndex = entity.LaggingIndex;
                _dbCtx.SaveChanges();
            }            
        }

        public void DeleteChartdata(Models.Dashboard.Chartdata entity)
        {
            DeleteChartdataById(entity.Id);
        }

        public void DeleteChartdataById(int entityId)
        {
            var entity = _dbCtx.Chartdatas.Find(entityId);
            _dbCtx.Chartdatas.Remove(entity);
            _dbCtx.SaveChanges();
        }

        public Chartdata GetChartdataById(int entityId)
        {
            return _dbCtx.Chartdatas.Find(entityId);
        }

        public List<Chartdata> GetAllChartdatas()
        {
            return _dbCtx.Chartdatas.ToList();
        }
    }
}