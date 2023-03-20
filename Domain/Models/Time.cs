using Microsoft.VisualBasic;

namespace Domain.Models;

public class Time
{
    public int Id { get; set; }
    public DateFormat DateStart { get; set; }
    public DateFormat DateEnd { get; set; }
    public int CountDay { get; set; }
    public int CountRemainDay { get; set; }
    public int EquipmentId { get; set; }
    public int OrderId { get; set; }
    public int SubscriptionId { get; set; }
}