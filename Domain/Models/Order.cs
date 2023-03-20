namespace Domain.Models;

public class Order
{
    public int Id { get; set; }

    // Тип услуги
    public int Service { get; set; }
    public string? Description { get; set; }
    public bool GetOrder { get; set; }
    public bool CompletedOrder { get; set; }
    public int Price { get; set; }
    
    public int UserId { get; set; } 
    public int CompanyId { get; set; } 
    public int HandcraftId { get; set; } 
}