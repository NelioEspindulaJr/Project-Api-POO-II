using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api2.Models;
using Microsoft.AspNetCore.Cors;
using api2.services;

namespace api2.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllHeaders")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly Context _context;
        private readonly Context _context2;
        private readonly PaymentService _paymentService;

        public class PaymentRequest
        {
            public Order Order { get; set; }
            public PaymentInfo PaymentInfo { get; set; }
            public List<ProductInfo> Products { get; set; }
        }

        public class ProductInfo
        {
            public int Id { get; set; }
            public int OnCartAmount { get; set; }
        }

        public class PayOrderRequest
        {
            public PaymentInfo PaymentInfo { get; set; }
            public Order Order { get; set; }
            public List<IProduct> Products { get; set; }
        }


        public class IProduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
            public string Sku { get; set; }
            public int Quantity { get; set; }
            public bool Available { get; set; }
            public int CategoryId { get; set; }
            public int OnCartAmount { get; set; }
        }

        public OrderController(Context context, PaymentService paymentService)
        {
            _context = context;

            _context2 = context;

            _paymentService = paymentService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.ToListAsync();
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPost("pay")]
        public async Task<ActionResult<Order>> PayForOrder([FromBody] PayOrderRequest request)
        {
            try
            {


                var newOrder = new Order
                {
                    UserId = request.Order.UserId,
                    Total = request.Order.Total,
                    Status = request.Order.Status,
                    PaymentType = request.Order.PaymentType,
                    DiscountId = request.Order?.DiscountId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };


                _context.Order.Add(newOrder);

                await _context.SaveChangesAsync();

                foreach (var productInfo in request.Products)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = newOrder.Id,
                        ProductId = productInfo.Id,
                        Quantity = productInfo.OnCartAmount,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _context.OrderItem.Add(orderItem);
                }

                await _context.SaveChangesAsync();

                ConfirmOrder(request.PaymentInfo, newOrder);

                return Ok(newOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        [HttpPost("pay/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public object ConfirmOrder(PaymentInfo paymentInfo, Order Order)
        {
            var order = Order;

            bool paymentSuccess = _paymentService.ProcessPayment(Order, paymentInfo);

            if (paymentSuccess)
            {

                order.Status = "Completed";

                _context.Update(order);

                _context.SaveChanges();

                return Ok(new { Message = "Pagamento bem-sucedido. Ordem concluída." });
            }
            else
            {
                return BadRequest(new { Message = "Falha no processamento do pagamento." });
            }
        }
    }
}
