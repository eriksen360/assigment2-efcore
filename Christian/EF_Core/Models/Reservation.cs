using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public bool Cancelled { get; set; }

        public int CanteenId { get; set; }
        public Canteen Canteen { get; set; }
        [ForeignKey("Customer")]
        public string AUID { get; set; }
        public Customer Customer { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
