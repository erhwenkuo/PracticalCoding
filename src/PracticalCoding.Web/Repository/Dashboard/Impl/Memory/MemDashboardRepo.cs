using PracticalCoding.Web.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Repository.Dashboard.Impl.Memory
{
    public class MemDashboardRepo : IDashboardRepo
    {
        private int idCounter = 0;
        private Dictionary<int, Chartdata> _memDataStore = new Dictionary<int, Chartdata>();

        public MemDashboardRepo()
        {
        }

        public int CreateChartdata(Chartdata entity)
        {
            idCounter++;
            entity.Id = idCounter;
            _memDataStore.Add(entity.Id, entity);
            return entity.Id;
        }

        public void UpdateChartdata(Chartdata entity)
        {
            _memDataStore[entity.Id] = entity;
        }

        public void DeleteChartdata(Chartdata entity)
        {
            DeleteChartdataById(entity.Id);
        }

        public void DeleteChartdataById(int entityId)
        {
            _memDataStore.Remove(entityId);
        }

        public Chartdata GetChartdataById(int entityId)
        {
            if (_memDataStore.ContainsKey(entityId))
                return _memDataStore[entityId];
            else
                return null;
        }

        public List<Chartdata> GetAllChartdatas()
        {
            return _memDataStore.Values.OrderBy(o => o.Id).ToList<Chartdata>();
        }
    }
}