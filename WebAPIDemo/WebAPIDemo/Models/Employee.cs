using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIDemo.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public String City { get; set; }
        public Boolean IsActive { get; set; }
    }
}