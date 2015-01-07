using NServiceKit.Redis;
using NServiceKit.Redis.Generic;
using PracticalCoding.Web.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Repository.Dashboard.Impl.Redis
{
    public class RedisDashboardRepo : IDashboardRepo
    {
        private IRedisClient _rdClient;
        private IRedisTypedClient<Chartdata> _rdChartdatas;

        public RedisDashboardRepo(IRedisClient rdClient)
        {
            _rdClient = rdClient;
            _rdChartdatas = _rdClient.As<Chartdata>();
        }

        public int CreateChartdata(Chartdata entity)
        {
            entity.Id = (int)_rdChartdatas.GetNextSequence();
            entity.Period_UTC = entity.parseDateToUTC(entity.Period);
            _rdChartdatas.Store(entity);
            return entity.Id;
        }

        public void UpdateChartdata(Chartdata entity)
        {
            entity.Period_UTC = entity.parseDateToUTC(entity.Period);
            _rdChartdatas.Store(entity);
        }

        public void DeleteChartdata(Chartdata entity)
        {
            _rdChartdatas.Delete(entity);
        }

        public void DeleteChartdataById(int entityId)
        {
            _rdChartdatas.DeleteById(entityId);
        }

        public Chartdata GetChartdataById(int entityId)
        {
            return _rdChartdatas.GetById(entityId);
        }

        public List<Chartdata> GetAllChartdatas()
        {
            return _rdChartdatas.GetAll().OrderBy(o => o.Id).ToList<Chartdata>();
        }
    }
}