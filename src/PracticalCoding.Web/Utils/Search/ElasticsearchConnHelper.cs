using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Utils.Search
{
    public class ElasticsearchConnHelper
    {
        private static Lazy<IElasticClient> lazyConnection = new Lazy<IElasticClient>(() =>
        {
            string connConfigKey = "elasticsearch:ConnectionConfig";
            string connConfig = "localhost:9200";
            string defaultIndex = "practicalcoding";

            IElasticClient esClient;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[connConfigKey]))
            {
                connConfig = ConfigurationManager.AppSettings[connConfigKey];
                //設定與Elasticsearch的連線
                var nodes = new List<Uri>();
                foreach (var node in connConfig.Split(';'))
                    nodes.Add(new Uri("http://"+node));

                var esConnectionPool = new SniffingConnectionPool(nodes.ToArray());
                var esSettings = new ConnectionSettings(esConnectionPool, defaultIndex);

                esClient = new ElasticClient(esSettings);
            }
            else
            {
                //設定與Elasticsearch的連線
                var node = new Uri("http://"+connConfig);
                var settings = new ConnectionSettings(node, defaultIndex);
                esClient = new ElasticClient(settings);
            }
            return esClient;
        });

        public static IElasticClient Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}