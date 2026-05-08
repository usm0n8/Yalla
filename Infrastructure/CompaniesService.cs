using System;
using System.Data;
using Dapper;
using Domain;

namespace Infrastructure;

public class CompniesServer
{
    DataContext context = new();
    public async Task<List<Company>> GetCompanies()
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var query = "select * from companies";
        var a = await conn.QueryAsync<Company>(query);
        return a.ToList();
    }
    public async Task<Company?> GetCompaniebyId(int id)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from companies where id = @id";
        var exists = await conn.ExecuteAsync(checking, new {id});
        if(exists == 0)
        {
            System.Console.WriteLine("Company is not found");
            return null;
        }

        var query = "select * from companies where id = @id";
        return await conn.QueryFirstOrDefaultAsync<Company>(query, new {id});
    }

    public async Task<bool> AddCompany(Company company)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        if (string.IsNullOrWhiteSpace(company.Address))
        {
            System.Console.WriteLine("Address is requared");
            return false;
        }
        if (string.IsNullOrWhiteSpace(company.Name))
        {
            System.Console.WriteLine("Name of company is requared");
            return false;
        }
        if (string.IsNullOrWhiteSpace(company.Email))
        {
            System.Console.WriteLine("Email is requared");
            return false;
        }
        if (string.IsNullOrWhiteSpace(company.Phone))
        {
            System.Console.WriteLine("Phone number is requared");
            return false;
        }

        var query = $@"insert into companies(name, address, phone, email, created_at, updated_at)
                        values(@name, @address, @phone, @email, @created_at, @updated_at)";
        await conn.ExecuteAsync(query, company);
        return true;    
    }

    public async Task<bool> UpdateCompany(Company company)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        if (string.IsNullOrWhiteSpace(company.Address))
        {
            System.Console.WriteLine("Address is requared");
            return false;
        }
        if (string.IsNullOrWhiteSpace(company.Name))
        {
            System.Console.WriteLine("Name of company is requared");
            return false;
        }
        if (string.IsNullOrWhiteSpace(company.Email))
        {
            System.Console.WriteLine("Email is requared");
            return false;
        }
        if (string.IsNullOrWhiteSpace(company.Phone))
        {
            System.Console.WriteLine("Phone number is requared");
            return false;
        }

        var checking = "select * from companies where name = @name";
        var exists = await conn.ExecuteAsync(checking, new{name = company.Name});
        if(exists != 0)
        {
            System.Console.WriteLine("This company already exists");
            return false;
        }

        var query = $@"insert into companies set name = @name, address = @address, phone = @phone, email = @email, created_at = @created_at, updated_at = @updated_at
                        where id = @id";
        await conn.ExecuteAsync(query, company);
        return true;
    }

    public async Task<bool> DeletCompany(int id)
    {
        using var conn = context.GetNpgsqlConnection();
        conn.Open();

        var checking = "select * from companies where id = @id";
        var exists = await conn.ExecuteAsync(checking, new{id});
        if(exists != 0)
        {
            System.Console.WriteLine("Company is not found");
            return false;
        }

        var query = "delete from companies where id = @id";
        await conn.ExecuteAsync(query, new{id});
        return true;
    }
}
