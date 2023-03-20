namespace Domain.Models;

/// <summary>
/// Подписка
/// </summary>
public class Subscription
{
    public int Id { get; set; }
    public int NumberSubscription { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Price { get; set; }
    public int CompanyId { get; set; }
    public int HandicraftId { get; set; }
}