namespace Domain.Models.Users;

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; } = "";
    public string? Description { get; set; } = "";
    public int Rating { get; set; }
    public string? Subscription { get; set; }
    public string? Phone { get; set; } = "";
    public string? Email { get; set; } = "";
    public string? Password { get; set; } = "";
    public string? Link { get; set; } = "";
    public string? CityName { get; set; } = "";
    public string? Street { get; set; } = "";
    public string? Home { get; set; } = "";
}