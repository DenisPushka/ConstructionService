namespace Domain.Models;

/// <summary>
/// Подписка
/// </summary>
public class Subscription
{
    public int Id { get; set; }
    public string? Description { get; set; } = string.Empty;
    public int Price { get; set; }
}