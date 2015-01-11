using CsvHelper;
using Nest;
using NServiceKit.Redis;
using PracticalCoding.Web.Models;
using PracticalCoding.Web.Models.Dashboard;
using PracticalCoding.Web.Repository.Dashboard.Impl.Elasticsearch;
using PracticalCoding.Web.Repository.Dashboard.Impl.Memory;
using PracticalCoding.Web.Utils.Cache;
using PracticalCoding.Web.Utils.Search;
using StackExchange.Redis;
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
        }
    }
}
