using System;

namespace Second.DTOs;

public class PaymentDto
{
    public int PaymentId { get; set; }
    public int VoucherId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Price { get; set; }
}