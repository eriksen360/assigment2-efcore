using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Staff
    {
        public string StaffId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Salary { get; set; }
        
        public int CanteenId { get; set; }
        public Canteen Canteen { get; set; }
    }
}
