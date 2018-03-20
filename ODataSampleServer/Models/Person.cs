using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataSampleServer.Models
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Quote { get; set; }
        public bool IsUnicorn { get; set; }
        public int LuckyNumber { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
