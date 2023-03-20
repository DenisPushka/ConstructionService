namespace Domain.Models;

public class Feedback
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int HandcraftId { get; set; }
    public int OrderId { get; set; }
    public string Description { get; set; } = string.Empty;
}