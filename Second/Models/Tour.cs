using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Second.Models
{
    public class Tour
    {
        [Key]
        [Required]
        public int TourId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Info { get; set; }
        [InverseProperty(nameof(Season.Tour))]
        public virtual List<Season> Seasons { get; set; } = new List<Season>();

    }
}