using System;

namespace Domain;

public class Order
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateOnly UpdatedAt { get; set; }
}
