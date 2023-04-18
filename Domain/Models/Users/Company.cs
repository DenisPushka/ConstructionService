namespace Domain.Models.Users;

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Subscription { get; set; }
    public string? Phone { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public string? Link { get; set; } = string.Empty;
    public string? CityName { get; set; } = string.Empty;
    public string? Street { get; set; } = string.Empty;
    public string? Home { get; set; } = string.Empty;
}