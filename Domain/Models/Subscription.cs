namespace Domain.Models;

/// <summary>
/// Подписка.
/// </summary>
public class Subscription
{
    public int Id { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; } = "";

    /// <summary>
    /// Стоимость.
    /// </summary>
    public int Price { get; set; }
}