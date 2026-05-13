using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController
{
    private readonly orderService _service;
    public OrdersController(orderService service)
    {
        _service = service;
    }

    [HttpGet("company/{id}")]
    public async Task<Order?> GetCompanyOrdersAsync(int id)
    {
        return await _service.GetCompanyOrdersAsync(id);
    }

    [HttpPost]
    public async Task<bool> CreateOrderAsync(Order order)
    {
        return await _service.CreateOrderAsync(order);
    }

    [HttpPut("{id}")]
    public async Task<bool> UpdateOrder(int id, Order order)
    {
        order.Id = id;

        return await _service.UpdateOrder(order);
    }

    [HttpGet("daily-summary")]
    public async Task<decimal> GetDailySummaryAsync(DateTime date)
    {
        return await _service.GetDailySummaryAsync(date);
    }
}