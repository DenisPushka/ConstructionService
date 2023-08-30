using System.Threading.Tasks;
using DataAccess.models;

namespace DataAccess.Interface;

/// <summary>
/// Интерфейс репозитория аунтефикации.
/// </summary>
public interface IAuthenticationRepository
{ 
    /// <summary>
    /// Аунтефикация.
    /// </summary>
    /// <param name="userAuthentication">Пользвоатель для аунтефикации.</param>
    /// <returns>true - в случае нахождения пользователя в системе.</returns>
    Task<bool> Authentication(UserAuthentication userAuthentication);
}