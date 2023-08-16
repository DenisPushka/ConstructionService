using DataAccess.Interface;
using DataAccess.models;

namespace DataAccess.Realization;

/// <summary>
/// Репозиторй для аунтефикации.
/// </summary>
public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly DataSql _context;

    public AuthenticationRepository(DataSql context) => _context = context;

    /// <summary>
    /// Аунтефикация.
    /// </summary>
    /// <param name="userAuthentication">Пользователя для аунтефикации.</param>
    /// <returns>true - если есть в системе.</returns>
    public async Task<bool> Authentication(UserAuthentication userAuthentication) =>
        await _context.Authentication(userAuthentication);
}