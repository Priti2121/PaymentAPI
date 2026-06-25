using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Data;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class PaymentController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public PaymentController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: api/Payment
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
            {
                return await _context.Payments.ToListAsync();
            }

            // GET: api/Payment/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Payment>> GetPayment(int id)
            {
                var payment = await _context.Payments.FindAsync(id);

                if (payment == null)
                {
                    return NotFound();
                }

                return payment;
            }

            // POST: api/Payment
            [HttpPost]
            public async Task<ActionResult<Payment>> PostPayment(Payment payment)
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPayment),
                    new { id = payment.Id }, payment);
            }

            // PUT: api/Payment/5
            [HttpPut("{id}")]
            public async Task<IActionResult> PutPayment(int id, Payment payment)
            {
                if (id != payment.Id)
                {
                    return BadRequest();
                }

                _context.Entry(payment).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }

            // DELETE: api/Payment/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePayment(int id)
            {
                var payment = await _context.Payments.FindAsync(id);

                if (payment == null)
                {
                    return NotFound();
                }

                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }

