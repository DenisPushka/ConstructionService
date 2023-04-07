using System.Text;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace Api.Models;

public class OrderFromVie
{
    public int Id { get; set; }
    public string MiniDescription { get; set; } = "";
    public string Description { get; set; } = "";
    public bool GetOrder { get; set; }
    public bool CompletedOrder { get; set; }
    public int Price { get; set; }
    public string? NameCity { get; set; } = "";
    public string? Date { get; set; } = "";
    public string? CategoryWork { get; set; } = "";
    public string? Work { get; set; } = "";
    public IFormFile? Example { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int HandcraftId { get; set; }

    public async Task<Order> OrderVieToOrder()
    {
        
        var order = new Order
        {
            Id = Id, MiniDescription = MiniDescription, Description = Description, GetOrder = GetOrder,
            CompletedOrder = CompletedOrder, Price = Price, NameCity = NameCity, Date = Date,
            CategoryWork = CategoryWork, UserId = UserId, Work = Work
        };
        if (order.Date == null)
            order.Date = "";
        if (order.NameCity == null)
            order.NameCity = "";
        if (order.Work == null)
            order.Work = "";

        if (Example != null)
        {
            var length = Example.Length;
            if (length > 0)
            {
                await using var fileStream = Example.OpenReadStream();
                var bytes = new byte[length];
                await fileStream.ReadAsync(bytes.AsMemory(0, (int)Example.Length));
                order.Example = bytes;
            }        }

        return order;
    }
}