using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Models.CacheModel
{
    public class Employee
    {
        public Employee() { }

        public Employee(int EmployeeId, string Name)
        {
            this.Id = EmployeeId;
            this.Name = Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}