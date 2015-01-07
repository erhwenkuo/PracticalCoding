using Nest;
using PracticalCoding.Web.Models.Dashboard;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Repository.Dashboard.Impl.Elasticsearch
{
    public class ElasticsearchDashboardRepo : IDashboardRepo
    {
        private IElasticClient _esClient;
        private IDatabase _rdClient;

        public ElasticsearchDashboardRepo(IElasticClient esClient
            , IDatabase rdClient)
        {
            this._esClient = esClient;
            this._rdClient = rdClient;
        }

        public int CreateChartdata(Chartdata entity)
        {
            var id = GetNextIdSeq();
            entity.Period_UTC = entity.parseDateToUTC(entity.Period);
            entity.Id = id;
            _esClient.Index<Chartdata>(entity);
            return id;            
        }

        public void UpdateChartdata(Chartdata entity)
        {
            entity.Period_UTC = entity.parseDateToUTC(entity.Period);
            _esClient.Index<Chartdata>(entity);
        }

        public void DeleteChartdata(Chartdata entity)
        {
            DeleteChartdataById(entity.Id);
        }

        public void DeleteChartdataById(int entityId)
        {
            _esClient.Delete<Chartdata>(entityId);
            RemoveId(entityId);
        }

        public Chartdata GetChartdataById(int entityId)
        {
            return _esClient.Source<Chartdata>(entityId);
        }

        public List<Chartdata> GetAllChartdatas()
        {
            var result = new List<Chartdata>();
            var ids = GetAllIds();
            if (ids == null || ids.Count() == 0)
                return result;

            var chartdatas = _esClient.GetMany<Chartdata>(ids);
            foreach (var obj in chartdatas)
                result.Add(obj.Source);

            return result.OrderBy(o => o.Id).ToList<Chartdata>();
        }

        private int GetNextIdSeq()
        {
            var IdSeq_Key = "Chartdata.IdSeq";
            var Ids_Key= "Chartdata.IdSet";
            var id = (int)_rdClient.StringIncrement(IdSeq_Key, 1);
            _rdClient.SetAdd(Ids_Key, id);
            return id;
        }

        private long[] GetAllIds()
        {
            var Ids_Key = "Chartdata.IdSet";
            var values = _rdClient.SetMembers(Ids_Key);
            var result = new long[values.Count()];

            for(int i=0; i<values.Count(); i++)
                result[i] = (long) values[i];

            return result;
        }

        private bool RemoveId(int id)
        {
            var Ids_Key = "Chartdata.IdSet";
            return _rdClient.SetRemove(Ids_Key, id);
        }
    }
}