using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string MealType { get; set;}
        public string Name { get; set;}
        public int ?Quantity { get; set; }

        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
