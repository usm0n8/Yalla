using System;
using Dapper;
using Domain;

namespace Infrastructure;

public class SubscriptionsService(DataContext context)
{
    public async Task<Subscription?> GetCompanySubscriptionsAsync(int id)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from subscriptions where company_id = @id";
        var exists = await conn.ExecuteAsync(checking, new {id});
        if(exists == 0)
        {
            System.Console.WriteLine("Subscription is not found");
            return null;
        }

        var query = "select * from subscriptions where company_id = @id";
        return await conn.QueryFirstOrDefaultAsync<Subscription>(query, new{id});
    }

    public async Task<bool> CreateSubscriptionAsync(Subscription company)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();


        var query = $@"insert into subscriptions(company_id, plan_type, meals_per_day, price, start_date, end_date, is_active)
                        values(@company_id, @plan_type, @meals_per_day, @price, @start_date, @end_date, @is_active)";
        await conn.ExecuteAsync(query, company);
        return true;    
    }

    public async Task<bool> UpdateSubscriptionStatusAsync(int id, bool isActive)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from subscriptions where id = @id";
        var exists = await conn.ExecuteAsync(checking, new{id});
        if(exists != 0)
        {
            System.Console.WriteLine("Subscription is not found");
            return false;
        }

        var query = $@"insert into subscriptions set is_active = @isActive
                        where id = @id";
        await conn.ExecuteAsync(query, new{isActive});
        return true;
    }

    public async Task<List<Subscription>> GetActiveSubscriptionsAsync()
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var query = "select * from subscriptions where is_active is true";
        var a = await conn.QueryAsync<Subscription>(query);
        return a.ToList();
    }
}
