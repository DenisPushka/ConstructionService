namespace Domain.Models.Service;

/// <summary>
/// Тип оборудования.
/// </summary>
public class TypeEquipment
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название.
    /// </summary>
    public string? Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Id сервиса.
    /// </summary>
    public int ServiceId { get; set; }
}