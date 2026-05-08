using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly MenusService _service = new();

    [HttpGet("active")]
    public async Task<ActionResult<List<Menu>>> GetActiveMenuAsync()
    {
        var result = await _service.GetActiveMenuAsync();
        return Ok(result);
    }

    [HttpGet("by-date")]
    public async Task<ActionResult<Menu>> GetMenuByDateAsync(DateTime date)
    {
        var result = await _service.GetMenuByDateAsync(date);

        if (result == null)
        {
            return NotFound("Menu not found");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddMenu(Menu menu)
    {
        var result = await _service.AddMenu(menu);

        if (!result)
        {
            return BadRequest("Menu not added");
        }

        return Ok("Menu added");
    }

    [HttpPost("{menuId}/items")]
    public async Task<ActionResult> AddMenuItemAsync(int menuId, MenuItem menu)
    {
        menu.MenuId = menuId;

        var result = await _service.AddMenuItemAsync(menuId, menu);

        if (!result)
        {
            return BadRequest("Menu item not added");
        }

        return Ok("Menu item added");
    }

    [HttpGet("categories")]
    public async Task<ActionResult<List<MenuItem>>> GetMenuCategoriesAsync()
    {
        var result = await _service.GetMenuCategoriesAsync();
        return Ok(result);
    }
}