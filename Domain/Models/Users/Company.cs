namespace Domain.Models.Users;

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int Rating { get; set; }

    // Контакт компании
    public int ContactCompanyId { get; set; }
    public string? Phone { get; set; } = string.Empty;
    
    // todo вынести логины и пароли в отедельную таблицу
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public string? Link { get; set; } = string.Empty;
    
    // todo Адрес
    public string? CityName { get; set; } = string.Empty;
    public string? Street { get; set; } = string.Empty;
    public string? Home { get; set; } = string.Empty;
}