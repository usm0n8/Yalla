using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly SubscriptionsService _service = new();

    [HttpGet("company/{id}")]
    public async Task<ActionResult<Subscription>> GetCompanySubscriptionsAsync(int id)
    {
        var result = await _service.GetCompanySubscriptionsAsync(id);

        if (result == null)
        {
            return NotFound("Subscription not found");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateSubscriptionAsync(Subscription subscription)
    {
        var result = await _service.CreateSubscriptionAsync(subscription);

        if (!result)
        {
            return BadRequest("Subscription not created");
        }

        return Ok("Subscription created");
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult> UpdateSubscriptionStatusAsync(int id, bool isActive)
    {
        var result = await _service.UpdateSubscriptionStatusAsync(id, isActive);

        if (!result)
        {
            return BadRequest("Subscription not updated");
        }

        return Ok("Subscription updated");
    }

    [HttpGet("active")]
    public async Task<ActionResult<List<Subscription>>> GetActiveSubscriptionsAsync()
    {
        var result = await _service.GetActiveSubscriptionsAsync();

        return Ok(result);
    }
}