using System.Reflection;
using Microsoft.VisualBasic;

namespace Domain.Models.Users;

public class User
{
    public int Id { get; set; }
    public int CountMadeOrders { get; set; }

    // Контакт пользователя
    public string? LastName { get; set; } = "";
    public string? Name { get; set; } = "";
    public string? Patronymic { get; set; } = "";
    public string? DateOfBrith { get; set; } = "";
    public string? Phone { get; set; } = "";
    public string? CityName { get; set; } = "";
    public byte[]? Image { get; set; }
    public string? Email { get; set; } = "";
    public string? Password { get; set; }  = "";
    public string? LinkTelegram { get; set; } = "";
    public string? LinkVk { get; set; } = "";
}