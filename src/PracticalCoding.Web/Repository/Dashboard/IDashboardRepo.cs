using PracticalCoding.Web.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticalCoding.Web.Repository.Dashboard
{
    public interface IDashboardRepo
    {
        int CreateChartdata(Chartdata entity);

        void UpdateChartdata(Chartdata entity);

        void DeleteChartdata(Chartdata entity);

        void DeleteChartdataById(int entityId);

        Chartdata GetChartdataById(int entityId);

        List<Chartdata> GetAllChartdatas();
    }
}
