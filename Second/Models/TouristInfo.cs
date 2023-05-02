using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Second.Models
{
    public class TouristInfo
    {
        [Key]
        public int TouristId { get; set; }
        public string Passport { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }
        [ForeignKey("TouristId")]
        [InverseProperty(nameof(Models.Tourist.TouristInfos))]
        public Tourist Tourist { get; set; } = new Tourist();
        [InverseProperty(nameof(Models.Voucher.TouristInfo))]
        public virtual List<Voucher> Vouchers { get; set; } = new List<Voucher>();
    }

}