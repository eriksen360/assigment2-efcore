using System.ComponentModel.DataAnnotations;

namespace EF_Core.Models
{
    public class Customer
    {
        [Key]
        public string AUID { get; set; }
        public string Name { get; set; }
    }
}
