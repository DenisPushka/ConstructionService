using DataAccess.models;
using Domain.Models.Service;

namespace Api.Models;

/// <summary>
/// Добаляльщик работы. 
/// </summary>
public class AddWorkCompany
{
    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    public UserAuthentication UserAuthentication { get; set; }

    /// <summary>
    /// Работа.
    /// </summary>
    public Work Work { get; set; }
}