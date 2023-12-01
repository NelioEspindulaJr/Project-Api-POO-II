using System;
using api2.Models;

namespace api2.services;
public class PaymentService
{
    private readonly Context _context;

    public PaymentService(Context context)
    {
        _context = context;
    }

    public bool ProcessPayment(Order order, PaymentInfo paymentInfo)
    {
        var payment = new Payment
        {
            OrderId = order.Id,
            Amount = order.Total,
            Provider = paymentInfo.Provider,
            TransactionId = GenerateTransactionId(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Payment.Add(payment);

        _context.SaveChanges();

        return true;
    }

    private static string GenerateTransactionId()
    {
        return Guid.NewGuid().ToString();
    }
}
