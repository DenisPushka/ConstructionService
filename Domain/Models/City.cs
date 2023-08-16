namespace Domain.Models;

/// <summary>
/// Город.
/// </summary>
public class City
{
    public int Id { get; set; }

    /// <summary>
    /// Название города.
    /// </summary>
    public string? NameCity { get; set; } = "";
}