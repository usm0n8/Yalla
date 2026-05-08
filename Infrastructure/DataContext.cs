using System;
using System.Data.Common;
using Npgsql;

namespace Infrastructure;

public class DataContext
{
    const string connectionString = "Server=localhost;Database=Yalla;User Id=postgres;Password=mansur211";
    public NpgsqlConnection GetNpgsqlConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}
