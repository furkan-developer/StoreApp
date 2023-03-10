using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }
    }
}