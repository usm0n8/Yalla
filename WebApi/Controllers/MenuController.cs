using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController
{
    private readonly MenusService _service;
    public MenuController(MenusService service)
    {
        _service = service;
    }

    [HttpGet("active")]
    public async Task<List<Menu>> GetActiveMenuAsync()
    {
        return await _service.GetActiveMenuAsync();
    }

    [HttpGet("by-date")]
    public async Task<Menu?> GetMenuByDateAsync(DateTime date)
    {
        return await _service.GetMenuByDateAsync(date);
    }

    [HttpPost]
    public async Task<bool> AddMenu(Menu menu)
    {
        return await _service.AddMenu(menu);
    }

    [HttpPost("{menuId}/items")]
    public async Task<bool> AddMenuItemAsync(int menuId, MenuItem menu)
    {
        menu.MenuId = menuId;

        return await _service.AddMenuItemAsync(menuId, menu);
    }

    [HttpGet("categories")]
    public async Task<List<MenuItem>> GetMenuCategoriesAsync()
    {
        return await _service.GetMenuCategoriesAsync();
    }
}