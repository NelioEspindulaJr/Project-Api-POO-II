using System;

namespace api2.Models;

public class Discount
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal DiscountPercent { get; set; }
    public int AvailableUses { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}