using PracticalCoding.Web.Models.CacheModel;
using PracticalCoding.Web.Models.CodeFirst;
using PracticalCoding.Web.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Models
{
    public class EF6DbContext : DbContext
    {  
        public EF6DbContext()
            : base("name=EF6DbContext")
        {
            //透過以下的設定可以讓Entity Framework把最後產生的SQL statement透在Debug Console
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        #region Entities for Session_04

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<LazyAuthor> LazyAuthors { get; set; }

        public DbSet<LazyBook> LazyBooks { get; set; }

        public DbSet<CirRefAuthor> CirRefAuthors { get; set; }

        public DbSet<CirRefBook> CirRefBooks { get; set; }

        #endregion Entities for Session_04

        #region Entities for Session_05
        public DbSet<Employee> Employees { get; set; }

        #endregion Entities for Session_05

        public DbSet<Chartdata> Chartdatas { get; set; }


    }
}


