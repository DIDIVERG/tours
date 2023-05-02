using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Documents;


namespace Second.Models
{
    public class Voucher
    {
        [Key]
        [Required]
        public int VoucherId { get; set; }
        public int TouristId { get; set; }
        public int SeasonId { get; set; }

        [ForeignKey("TouristId")]
        [InverseProperty(nameof(Models.TouristInfo.Vouchers))]
        public virtual TouristInfo TouristInfo { get; set; } = new TouristInfo();

        [ForeignKey("SeasonId")]
        [InverseProperty(nameof(Models.Season.Vouchers))]
        public virtual Season Season { get; set; } = new Season();
        
        [InverseProperty(nameof(Models.Payment.Voucher))]
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();
    }
}