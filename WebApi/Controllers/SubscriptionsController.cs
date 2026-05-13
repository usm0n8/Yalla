using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController
{
    private readonly SubscriptionsService _service;
    public SubscriptionsController(SubscriptionsService service)
    {
        _service = service;
    }

    [HttpGet("company/{id}")]
    public async Task<Subscription?> GetCompanySubscriptionsAsync(int id)
    {
        return await _service.GetCompanySubscriptionsAsync(id);
    }

    [HttpPost]
    public async Task<bool> CreateSubscriptionAsync(Subscription subscription)
    {
        return await _service.CreateSubscriptionAsync(subscription);
    }

    [HttpPut("{id}/status")]
    public async Task<bool> UpdateSubscriptionStatusAsync(int id, bool isActive)
    {
        return await _service.UpdateSubscriptionStatusAsync(id, isActive);
    }

    [HttpGet("active")]
    public async Task<List<Subscription>> GetActiveSubscriptionsAsync()
    {
        return await _service.GetActiveSubscriptionsAsync();
    }
}