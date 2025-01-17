using System;
using FetchBackendTestRyanMurphy;
using FetchBackEndTestRyanMurphy.Models;
using Microsoft.AspNetCore.Mvc;

namespace FetchBackEndTestRyanMurphy.Controllers
{
    [ApiController]
    [Route("receipts")]
    public class ReceiptController : ControllerBase
    {
        private static readonly Dictionary<string, (Receipt Receipt, int Points)> Receipts = new();

        [HttpPost("process")]
        public IActionResult ProcessReceipt([FromBody] Receipt receipt)
        {
            if (receipt == null || string.IsNullOrWhiteSpace(receipt.Retailer))
            {
                return BadRequest(new { message = "Invalid receipt data." });
            }

            string id = Guid.NewGuid().ToString();

            try
            {
                ApplyRules rules = new();
                int points = rules.Calculate(receipt);

                Receipts[id] = (receipt, points);

                return Ok(new { id });
            }
            
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }

        [HttpGet("{id}/points")]
        public IActionResult GetPoints(string id)
        {
            if (Receipts.TryGetValue(id, out var entry))
            {
                return Ok(new { points = entry.Points });
            }

            return NotFound(new { message = "Receipt not found." });
        }
    }
}
