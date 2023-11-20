using System;

namespace api2.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string SKU { get; set; }
    public int Quantity { get; set; }
    public bool Available { get; set; }
    public int CategoryId { get; set; }
    public string Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Category Category { get; set; }
}