using DataAccess.Interface;
using DataAccess.models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IUserRepository _userRepository;

    public AuthorizationController(IAuthenticationRepository authenticationRepository, IUserRepository userRepository)
    {
        _authenticationRepository = authenticationRepository;
        _userRepository = userRepository;
    }

    // Аунтефикация
    [HttpPost("authentication")]
    public async Task<User> CheckAuthentication([FromForm] UserAuthentication userAuthentication)
    {
        return await _userRepository.GetUser(userAuthentication);
    }
}