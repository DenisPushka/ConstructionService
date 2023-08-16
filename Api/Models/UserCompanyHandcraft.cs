using Domain.Models.Users;

namespace Api.Models;

/// <summary>
/// Помощник в определения типа пользователя.
/// </summary>
public class UserCompanyHandcraft
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Компания.
    /// </summary>
    public Company Company { get; set; }

    /// <summary>
    /// Ремесленник.
    /// </summary>
    public Handcraft Handcraft { get; set; }
}