using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataSampleServer.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsUnicorn { get; set; }
        public int LuckyNumber { get; set; }
    }
}
