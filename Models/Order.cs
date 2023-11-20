using System;
using System.Collections.Generic;

namespace api2.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
    public string PaymentType { get; set; }
    public int? DiscountId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; }
    public Discount Discount { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public List<Payment> Payments { get; set; } = null;
}