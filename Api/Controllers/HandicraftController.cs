using DataAccess.Interface;
using DataAccess.models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для ремесленника.
/// </summary>
[ApiController, Route("api/[controller]")]
public class HandicraftController : ControllerBase
{
    /// <summary>
    /// Репозиторий ремесленника.
    /// </summary>
    private readonly IHandcraftRepository _handcraftRepository;
    
    public HandicraftController(IHandcraftRepository handcraftRepository) => _handcraftRepository = handcraftRepository;

    /// <summary>
    /// Получение ремесленника.
    /// </summary>
    /// <param name="user">Пользователь для авторизации.</param>
    /// <returns>Ремесленник.</returns>
    [HttpPost("GetHandicraft")]
    public async Task<Handcraft> GetHandicraft([FromForm] UserAuthentication user)
    {
        return await _handcraftRepository.GetHandcraft(user);
    }
}