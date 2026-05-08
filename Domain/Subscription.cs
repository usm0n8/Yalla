using System;

namespace Domain;

public class Subscription
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string? PlanType { get; set; }
    public int MealsPerDay { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
