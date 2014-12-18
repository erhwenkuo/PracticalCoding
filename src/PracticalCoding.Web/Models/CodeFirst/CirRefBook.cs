using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticalCoding.Web.Models.CodeFirst
{
    public class CirRefBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }       
 
        // Foreign Key
        public int AuthorId { get; set; }

        // Navigation property
        public virtual CirRefAuthor Author { get; set; }
    }
}


