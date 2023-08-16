namespace Domain.Models.Users;

/// <summary>
/// Компания.
/// </summary>
public class Company
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название.
    /// </summary>
    public string? Name { get; set; } = "";

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; } = "";

    /// <summary>
    /// Рейтинг.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Подписка.
    /// </summary>
    public string? Subscription { get; set; }

    /// <summary>
    /// Телефон.
    /// </summary>
    public string? Phone { get; set; } = "";

    /// <summary>
    /// Почта.
    /// </summary>
    public string? Email { get; set; } = "";

    /// <summary>
    /// Паоль.
    /// </summary>
    public string? Password { get; set; } = "";

    /// <summary>
    /// Ссылка.
    /// </summary>
    public string? Link { get; set; } = "";

    /// <summary>
    /// Название города.
    /// </summary>
    public string? CityName { get; set; } = "";

    /// <summary>
    /// Улица.
    /// </summary>
    public string? Street { get; set; } = "";

    /// <summary>
    /// Дом.
    /// </summary>
    public string? Home { get; set; } = "";
}