using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Api.Models;

public class OrderFromVie
{
    public int Id { get; set; } = 0;
    public string MiniDescription { get; set; } = "";
    public string Description { get; set; } = "";
    public bool GetOrder { get; set; }
    public bool CompletedOrder { get; set; }
    public int Price { get; set; } = 0;

    public string? NameCity { get; set; } = "";

    // public Time? Time { get; set; } = new Time();
    public string? DateStart { get; set; } = "";
    public string? DateEnd { get; set; } = "";
    public int CategoryWork { get; set; } = 0;
    public int Work { get; set; } = 0;
    public IFormFile? Photo { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; } = 0;
    public int HandcraftId { get; set; } = 0;

    public async Task<Order> OrderVieToOrder()
    {
        // todo накинуть проверку
        var time = new Time { DateStart = DateStart, DateEnd = DateEnd };

        var order = new Order
        {
            Id = Id, MiniDescription = MiniDescription, Description = Description, GetOrder = GetOrder,
            CompletedOrder = CompletedOrder, Price = Price, NameCity = NameCity, Time = time,
            CategoryJob = CategoryWork.ToString(), UserId = UserId, Job = Work.ToString()
        };
        order.NameCity ??= "";
        order.CategoryJob ??= "";
        order.Job ??= "";

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
            order.Photo = new byte[0];
        }

        return order;
    }
}