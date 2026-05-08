using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly orderService _service = new();

    [HttpGet("company/{id}")]
    public async Task<ActionResult<Order>> GetCompanyOrdersAsync(int id)
    {
        var result = await _service.GetCompanyOrdersAsync(id);

        if (result == null)
        {
            return NotFound("Order not found");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrderAsync(Order order)
    {
        var result = await _service.CreateOrderAsync(order);

        if (!result)
        {
            return BadRequest("Order not created");
        }

        return Ok("Order created");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, Order order)
    {
        order.Id = id;

        var result = await _service.UpdateOrder(order);

        if (!result)
        {
            return BadRequest("Order not updated");
        }

        return Ok("Order updated");
    }

    [HttpGet("daily-summary")]
    public async Task<ActionResult<decimal>> GetDailySummaryAsync(DateTime date)
    {
        var result = await _service.GetDailySummaryAsync(date);

        return Ok(result);
    }
}