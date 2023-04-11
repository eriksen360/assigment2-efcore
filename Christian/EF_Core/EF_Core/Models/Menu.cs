using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuType { get; set; }
        public DateTime Date { get; set; }

        public int CanteenId { get; set; }
        public Canteen Canteen { get; set; }
    }
}
