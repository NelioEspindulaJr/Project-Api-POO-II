using System;

namespace api2.Models
{
    public class ConfirmOrderRequest
    {
        public PaymentInfo PaymentInfo { get; set; }
        public Order Order { get; set; }
    }
}