using System;

namespace api2.Models;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Provider { get; set; }
    public string TransactionId { get; set; }
    public int OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Order Order { get; set; }
}