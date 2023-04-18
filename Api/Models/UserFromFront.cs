using System.Runtime.InteropServices.JavaScript;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;
using Microsoft.AspNetCore.Http;

namespace Api.Models;

public class UserFromFront
{
    public int Id { get; set; }
    public int CountMadeOrders { get; set; }

    // Контакт пользователя
    public string? LastName { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Patronymic { get; set; } = string.Empty;
    public string? DateOfBrith { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? CityName { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public string? LinkTelegram { get; set; } = string.Empty;
    public string? LinkVk { get; set; } = string.Empty;

    public async Task<User> UserFromFrontToOrder()
    {
        var user = new User
        {
            Id = Id, CountMadeOrders = CountMadeOrders, LastName = LastName, Name = Name, 
            CityName = CityName, Email = Email, Password = Password
        };
        user.DateOfBrith ??= "";
        user.Patronymic ??= "";
        user.Phone ??= "";
        
        if (Image != null)
        {
            var length = Image.Length;
            if (length > 0)
            {
                await using var fileStream = Image.OpenReadStream();
                var bytes = new byte[length];
                await fileStream.ReadAsync(bytes.AsMemory(0, (int)Image.Length));
                user.Image = bytes;
            }
        }

        return user;
    }
}