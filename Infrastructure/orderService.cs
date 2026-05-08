using System;
using Dapper;
using Domain;

namespace Infrastructure;

public class orderService
{
    DataContext context = new();

    public async Task<Order?> GetCompanyOrdersAsync(int id)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from orders where company_id = @id";
        var exists = await conn.ExecuteAsync(checking, new {id});
        if(exists == 0)
        {
            System.Console.WriteLine("Order is not found");
            return null;
        }

        var query = "select * from orders where company_id = @id";
        return await conn.QueryFirstOrDefaultAsync<Order>(query, new {id});
    }

    public async Task<bool> CreateOrderAsync(Order company)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        if (string.IsNullOrWhiteSpace(company.Status))
        {
            System.Console.WriteLine("Status is requared");
            return false;
        }

        var query = $@"insert into orders(company_idv, order_date, status, total_amount, created_at, updated_at)
                        values(@company_id, @order_date, @status, @total_amount, @created_at, @updated_at)";
        await conn.ExecuteAsync(query, company);
        return true;    
    }

    public async Task<bool> UpdateOrder(Order company)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from oreders where id = @id";
        var exists = await conn.ExecuteAsync(checking, new{id = company.Id});
        if(exists != 0)
        {
            System.Console.WriteLine("Order is not found");
            return false;
        }

        var query = $@"insert into orders set company_id = @company_id, order_date = @order_date, status = @status, total_amount = @total_amount, created_at = @created_at, updated_at = @updated_at
                        where id = @id";
        await conn.ExecuteAsync(query, company);
        return true;
    }

    public async Task<decimal> GetDailySummaryAsync(DateTime date)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from oreders where order_date = @date";
        var exists = await conn.ExecuteAsync(checking, new{date});
        if(exists == 0)
        {
            System.Console.WriteLine("Order is not found");
            return 0;
        }

        var query = "select sum(total_amount) from orders where order_date = date";
        return await conn.ExecuteScalarAsync<decimal>(query, new {date});
    }

    
}
