namespace Domain.Models.Users;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    public int Id { get; set; }

    /// <summary>
    /// Количство сделанных заказов.
    /// </summary>
    public int CountMadeOrders { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? LastName { get; set; } = "";

    /// <summary>
    /// Имя.
    /// </summary>
    public string? Name { get; set; } = "";

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; } = "";

    /// <summary>
    /// День рождение.
    /// </summary>
    public string? DateOfBrith { get; set; } = "";

    /// <summary>
    /// Телефон.
    /// </summary>
    public string? Phone { get; set; } = "";

    /// <summary>
    /// Город.
    /// </summary>
    public string? CityName { get; set; } = "";

    /// <summary>
    /// Фото.
    /// </summary>
    public byte[]? Image { get; set; }

    /// <summary>
    /// Почта.
    /// </summary>
    public string? Email { get; set; } = "";

    /// <summary>
    /// Пароль.
    /// </summary>
    public string? Password { get; set; } = "";

    /// <summary>
    /// Ссылка на телеграм.
    /// </summary>
    public string? LinkTelegram { get; set; } = "";

    /// <summary>
    /// Ссылка на вк.
    /// </summary>
    public string? LinkVk { get; set; } = "";
}