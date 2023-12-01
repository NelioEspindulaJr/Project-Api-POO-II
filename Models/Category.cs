using System;

namespace api2.Models;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Available { get; set; }
    public string Image { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}