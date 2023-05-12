namespace Domain.Models;

/// <summary>
/// Подписка
/// </summary>
public class Subscription
{
    public int Id { get; set; }
    public string? Description { get; set; } = "";
    public int Price { get; set; }
}