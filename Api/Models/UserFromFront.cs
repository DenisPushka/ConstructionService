using System;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;
using Microsoft.AspNetCore.Http;

namespace Api.Models;

/// <summary>
/// Пользователь приходящий с front.
/// </summary>
public class UserFromFront
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Количество сделанных заказов.
    /// </summary>
    public int CountMadeOrders { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? LastName { get; set; } = string.Empty;

    /// <summary>
    /// Имя.
    /// </summary>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public string? DateOfBrith { get; set; } = string.Empty;

    /// <summary>
    /// Телефон.
    /// </summary>
    public string? Phone { get; set; } = string.Empty;

    /// <summary>
    /// Город.
    /// </summary>
    public string? CityName { get; set; } = string.Empty;

    /// <summary>
    /// Фото.
    /// </summary>
    public IFormFile? Image { get; set; }

    /// <summary>
    /// Почта.
    /// </summary>
    public string? Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль.
    /// </summary>
    public string? Password { get; set; } = string.Empty;

    /// <summary>
    /// Ссылка на телеграм.
    /// </summary>
    public string? LinkTelegram { get; set; } = string.Empty;

    /// <summary>
    /// Ссылка на вк.
    /// </summary>
    public string? LinkVk { get; set; } = string.Empty;

    /// <summary>
    /// Преобразование из пользователя для отображения в пользователя.
    /// </summary>
    /// <returns>Пользователь.</returns>
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