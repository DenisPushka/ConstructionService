namespace Domain.Models.Service;

/// <summary>
/// Категория работы.
/// </summary>
public class CategoryWork
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название категории.
    /// </summary>
    public string? Name { get; set; } = "";

    /// <summary>
    /// Id сервиса.
    /// </summary>
    public int ServiceId { get; set; }
}