using BrusnikaPay.Interfaces;
using BrusnikaPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrusnikaPay.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] PaymentRequest request)
    {
        var response = await paymentService.CreatePaymentAsync(request);
        return Ok(response);
    }
}
