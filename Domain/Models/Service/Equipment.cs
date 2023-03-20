namespace Domain.Models.Service;

public class Equipment
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int TypeEquipmentId { get; set; }
}