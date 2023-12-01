
using System;

namespace api2.Models;

public class PayOrderRequest
{
    public Order Order { get; set; }
    public PaymentInfo PaymentInfo { get; set; }
    public IProduct[] Products { get; set; }

}

public interface IProduct
{
    int Id { get; set; }
    string Name { get; set; }
    double Price { get; set; }
    string Sku { get; set; }
    int Quantity { get; set; }
    bool Available { get; set; }
    int CategoryId { get; set; }
    int OnCartAmount { get; set; }
}