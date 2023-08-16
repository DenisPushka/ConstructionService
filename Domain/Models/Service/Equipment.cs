namespace Domain.Models.Service;

/// <summary>
/// Оборудование.
/// </summary>
public class Equipment
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
    /// Тип оборудования.
    /// </summary>
    public int TypeEquipmentId { get; set; }
}