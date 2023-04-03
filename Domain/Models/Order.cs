using Microsoft.AspNetCore.Http;

namespace Domain.Models;

public class Order
{
    // TODO заголовок
    public int Id { get; set; }
    public string? MiniDescription { get; set; }
    public string? Description { get; set; }
    public bool GetOrder { get; set; }
    public bool CompletedOrder { get; set; }
    public int Price { get; set; }
    public string? NameCity { get; set; }
    public string? Date { get; set; }
    public string? CategoryWork { get; set; }
    public string? Work { get; set; }
    public IFormFile[]? Example { get; set; }
    public int UserId { get; set; } 
    public int CompanyId { get; set; } 
    public int HandcraftId { get; set; } 
}