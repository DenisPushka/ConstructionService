namespace Domain.Models.Service;

/// <summary>
/// Сервис.
/// </summary>
public class Service
{
    /// <summary>
    /// Id.
    /// </summary>
    public int ServiceId { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string? Name { get; set; } = "";

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; } = "";
}