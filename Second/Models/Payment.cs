using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Second.Models
{
    public class Payment
    {
        [Key]
        [Required]
        public int PaymentId { get; set; }
        public int VoucherId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("VoucherId")]
        [InverseProperty(nameof(Models.Voucher.Payments))]
        public virtual Voucher Voucher { get; set; } = new Voucher();

    }
}