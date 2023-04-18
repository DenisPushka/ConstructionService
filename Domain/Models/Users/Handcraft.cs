namespace Domain.Models.Users;

public class Handcraft
{
    public int Id { get; set; }
    public string? Description { get; set; } = string.Empty;

    public string? Name { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Patronymic { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public string? CityName { get; set; } = string.Empty;
    public string? LinkVk { get; set; } = string.Empty;
    public string? LinkTelegram { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Subscription { get; set; } = string.Empty;
}