using DataAccess.models;
using Domain.Models.Service;

namespace Api.Models;

/// <summary>
/// Добаляльщик оборудования.
/// </summary>
public class AddEquipmentCompany
{
    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    public UserAuthentication UserAuthentication { get; set; }
    
    /// <summary>
    /// Оборудование.
    /// </summary>
    public Equipment Equipment { get; set; }
}