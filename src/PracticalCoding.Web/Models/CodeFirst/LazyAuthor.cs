using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Models.CodeFirst
{
    public class LazyAuthor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

