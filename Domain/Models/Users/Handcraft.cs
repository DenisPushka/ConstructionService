namespace Domain.Models.Users;

public class Handcraft
{
    public int Id { get; set; }
    public string? Description { get; set; } = "";

    public string? Name { get; set; } = "";
    public string? LastName { get; set; } = "";
    public string? Patronymic { get; set; } = "";
    public string? Phone { get; set; } = "";
    public string? Email { get; set; } = "";
    public string? Password { get; set; } = "";
    public string? CityName { get; set; } = "";
    public string? LinkVk { get; set; } = "";
    public string? LinkTelegram { get; set; } = "";
    public int Rating { get; set; }
    public string? Subscription { get; set; } = "";
}