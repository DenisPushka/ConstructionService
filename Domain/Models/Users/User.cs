using System.Reflection;
using Microsoft.VisualBasic;

namespace Domain.Models.Users;

public class User
{
    public int Id { get; set; }
    public int CountMadeOrders { get; set; }

    // Контакт пользователя
    public string? LastName { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Patronymic { get; set; } = string.Empty;
    public string? DateOfBrith { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public int CityId { get; set; }

    public byte[]? Image { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; }  = string.Empty;
    public string? LinkTelegram { get; set; } = string.Empty;
    public string? LinkVk { get; set; } = string.Empty;
    
}