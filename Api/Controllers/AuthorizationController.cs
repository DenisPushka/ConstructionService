using System.Threading.Tasks;
using Api.Models;
using DataAccess.Interface;
using DataAccess.models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для авторизации.
/// </summary>
[ApiController, Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    /// <summary>
    /// Репозиторий атворизации.
    /// </summary>
    private readonly IAuthenticationRepository _authenticationRepository;

    /// <summary>
    /// Пользовательский репозиторий.
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Репозиторий компании.
    /// </summary>
    private readonly ICompanyRepository _companyRepository;

    /// <summary>
    /// Репозиторий ремесленника.
    /// </summary>
    private readonly IHandcraftRepository _handcraftRepository;

    /// <summary>
    /// Конструктор с 4 параметрами (все репозитории).
    /// </summary>
    public AuthorizationController(IAuthenticationRepository authenticationRepository, IUserRepository userRepository,
        ICompanyRepository companyRepository, IHandcraftRepository handcraftRepository)
    {
        _authenticationRepository = authenticationRepository;
        _userRepository = userRepository;
        _companyRepository = companyRepository;
        _handcraftRepository = handcraftRepository;
    }

    /// <summary>
    /// Проверка аунтефикации.
    /// </summary>
    /// <param name="userAuthentication">Аунтетификатор.</param>
    /// <returns>Объект авторизации (один из всех пользователей).</returns>
    [HttpPost("authentication")]
    public async Task<UserCompanyHandcraft> CheckAuthentication([FromForm] UserAuthentication userAuthentication)
    {
        var uch = new UserCompanyHandcraft();
        var user = await _userRepository.GetUser(userAuthentication);
        if (!user.Email.Equals(""))
        {
            uch.User = user;
            return uch;
        }

        var company = await _companyRepository.GetCompany(userAuthentication);
        if (!company.Email.Equals(""))
        {
            uch.Company = company;
            return uch;
        }

        var handcraft = await _handcraftRepository.GetHandcraft(userAuthentication);
        if (!handcraft.Email.Equals(""))
        {
            uch.Handcraft = handcraft;
            return uch;
        }

        return uch;
    }
}