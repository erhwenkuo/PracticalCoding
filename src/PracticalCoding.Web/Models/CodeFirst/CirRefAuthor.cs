using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Models.CodeFirst
{
    public class CirRefAuthor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<CirRefBook> Books { get; set; }
    }
}

