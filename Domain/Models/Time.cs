namespace Domain.Models;

public class Time
{
    public int Id { get; set; } = 0;
    public string? DateStart { get; set; } = "";
    public string? DateEnd { get; set; } = "";
    public int CountDay { get; set; } = 0;
    public int CountRemainDay { get; set; } = 0;
    public int EquipmentId { get; set; } = 0;
    public int JobId { get; set; } = 0;
}