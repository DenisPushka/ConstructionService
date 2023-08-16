namespace Domain.Models.Users;

/// <summary>
/// Ремесленник.
/// </summary>
public class Handcraft
{
    public int Id { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; } = "";

    /// <summary>
    /// Имя.
    /// </summary>
    public string? Name { get; set; } = "";

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? LastName { get; set; } = "";

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; } = "";

    /// <summary>
    /// Телефон.
    /// </summary>
    public string? Phone { get; set; } = "";

    /// <summary>
    /// Почта.
    /// </summary>
    public string? Email { get; set; } = "";

    /// <summary>
    /// Пароль.
    /// </summary>
    public string? Password { get; set; } = "";

    /// <summary>
    /// Город.
    /// </summary>
    public string? CityName { get; set; } = "";

    /// <summary>
    /// Ссылка на вк.
    /// </summary>
    public string? LinkVk { get; set; } = "";

    /// <summary>
    /// Ссылка на телеграм.
    /// </summary>
    public string? LinkTelegram { get; set; } = "";

    /// <summary>
    /// Рейтинг.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Подписка.
    /// </summary>
    public string? Subscription { get; set; } = "";
}