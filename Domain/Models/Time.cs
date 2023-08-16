namespace Domain.Models;

/// <summary>
/// Время.
/// </summary>
public class Time
{
    public int Id { get; set; } = 0;

    /// <summary>
    /// Дата начала.
    /// </summary>
    public string? DateStart { get; set; } = "";

    /// <summary>
    /// Дата окончания.
    /// </summary>
    public string? DateEnd { get; set; } = "";

    /// <summary>
    /// Количество дней в этом промежутке.
    /// </summary>
    public int CountDay { get; set; } = 0;

    /// <summary>
    /// Количество пройденных дней.
    /// </summary>
    public int CountRemainDay { get; set; } = 0;

    public int EquipmentId { get; set; } = 0;
    public int JobId { get; set; } = 0;
}