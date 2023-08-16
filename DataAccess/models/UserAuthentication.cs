namespace DataAccess.models;

/// <summary>
/// Пользователь авторизации.
/// </summary>
public class UserAuthentication
{
    /// <summary>
    /// Логин.
    /// </summary>
    public string? Login { get; set; } = string.Empty;
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string? Password { get; set; } = string.Empty;
}