using System;
using Dapper;
using Domain;

namespace Infrastructure;

public class MenusService
{
    DataContext context = new();
    public async Task<List<Menu>> GetActiveMenuAsync()
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var query = "select * from menus where is_active is true";
        var a = await conn.QueryAsync<Menu>(query);
        return a.ToList();
    }
    public async Task<Menu?> GetMenuByDateAsync(DateTime date)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from menus where menu_date = @date";
        var exists = await conn.ExecuteAsync(checking, new {date});
        if(exists == 0)
        {
            System.Console.WriteLine("Menu is not found");
            return null;
        }

        var query = "select * from menus where menu_date = @date";
        return await conn.QueryFirstOrDefaultAsync<Menu>(query, new {date});
    }

    public async Task<bool> AddMenu(Menu menu)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();


        var query = $@"insert into menus(menu_date, is_active, created_at, updated_at)
                        values(@menu_date, @is_active, @created_at, @updated_at)";
        await conn.ExecuteAsync(query, menu);
        return true;    
    }

    public async Task<bool> AddMenuItemAsync(int menuId, MenuItem menu)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from menu_item where menu_id = @menu_id";
        var exists = await conn.ExecuteAsync(checking, new{menu_id = menu.MenuId});
        if(exists != 0)
        {
            System.Console.WriteLine("Menu is not found");
            return false;
        }

        var query = $@"insert into menu_item set name = @name, description = @description, price = @price, category = @category, created_at = @created_at, updated_at = @updated_at
                        where menu_id = @menu_id";
        await conn.ExecuteAsync(query, menu);
        return true;
    }

    public async Task<List<MenuItem>> GetMenuCategoriesAsync()
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var query = "select category from menu_item";
        var a = await conn.QueryAsync<MenuItem>(query);
        return a.ToList();
    }
}
