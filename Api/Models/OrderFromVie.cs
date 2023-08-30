using System;
using System.Threading.Tasks;
using DataAccess.models;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Api.Models;

/// <summary>
/// Отображение заказа.
/// </summary>
public class OrderFromVie
{
    /// <summary>
    /// Миниописание.
    /// </summary>
    public string? MiniDescription { get; set; } = "";

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; } = "";

    /// <summary>
    /// Заказ взят.
    /// </summary>
    public bool GetOrder { get; set; }

    /// <summary>
    /// Заказ выполнен.
    /// </summary>
    public bool CompletedOrder { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public int Price { get; set; } = 0;

    /// <summary>
    /// Город.
    /// </summary>
    public string? NameCity { get; set; } = "";

    /// <summary>
    /// Дата начала работы.
    /// </summary>
    public string? DateStart { get; set; } = "";

    /// <summary>
    /// Дата окончания работы.
    /// </summary>
    public string? DateEnd { get; set; } = "";

    /// <summary>
    /// Id категории работы.
    /// </summary>
    public int CategoryWork { get; set; } = 0;

    /// <summary>
    /// Id работы.
    /// </summary>
    public int Work { get; set; } = 0;

    /// <summary>
    /// Фото.
    /// </summary>
    public IFormFile? Photo { get; set; }

    /// <summary>
    /// Логин.
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Преобразование из отображаемого заказа в заказ.
    /// </summary>
    /// <returns>Заказ.</returns>
    public async Task<Order> OrderVieToOrder()
    {
        var time = new Time { DateStart = DateStart, DateEnd = DateEnd };
        var order = new Order
        {
            Id = 0, MiniDescription = MiniDescription, Description = Description, GetOrder = GetOrder,
            CompletedOrder = CompletedOrder, Price = Price, NameCity = NameCity, Time = time,
            CategoryJob = CategoryWork.ToString(), Job = Work.ToString()
        };
        order.NameCity ??= "";
        order.CategoryJob ??= "";
        order.Job ??= "";
        time.DateStart ??= "";
        time.DateEnd ??= "";

        if (Photo != null)
        {
            var length = Photo.Length;
            if (length > 0)
            {
                await using var fileStream = Photo.OpenReadStream();
                var bytes = new byte[length];
                await fileStream.ReadAsync(bytes.AsMemory(0, (int)Photo.Length));
                order.Photo = bytes;
            }
        }
        else
        {
            order.Photo = Array.Empty<byte>();
        }

        return order;
    }

    /// <summary>
    /// Создание объекта для проверки авторизации.
    /// </summary>
    /// <returns>Пользователь для авторизации.</returns>
    public Task<UserAuthentication> ToUserAuthentication() =>
        Task.FromResult(new UserAuthentication { Login = Login, Password = Password });
}