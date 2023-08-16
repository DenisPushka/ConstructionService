namespace Domain.Models.Service;

/// <summary>
/// Работа.
/// </summary>
public class Work
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string? Name { get; set; } = "";

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; } = "";

    /// <summary>
    /// Id категории работы.
    /// </summary>
    public int CategoryWorkId { get; set; }
}