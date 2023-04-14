using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Canteen
    {
        public int CanteenId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Staff> Staffs { get; set; }
    }
}
