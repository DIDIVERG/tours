using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Second.Models
{
    public class Season
    {
        [Key]
        [Required]
        public int SeasonId { get; set; }
        public int TourId { get; set; }
        public int SeatAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Closed { get; set; }
        [ForeignKey("TourId")]
        [InverseProperty(nameof(Models.Tour.Seasons))]
        public virtual Tour Tour { get; set; } = new Tour();
        [InverseProperty(nameof(Models.Voucher.Season))]
        public virtual List<Voucher> Vouchers { get; set; } = new List<Voucher>();

           
        
    }
}