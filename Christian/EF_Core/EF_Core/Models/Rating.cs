using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int RatingValue { get; set; }
        public DateTime Date { get; set; }

        public int CanteenId { get; set; }
        public Canteen Canteen { get; set; }
        [ForeignKey("Customer")]
        public string CprNumber { get; set; }
        public Customer Customer { get; set; }
    }
}
