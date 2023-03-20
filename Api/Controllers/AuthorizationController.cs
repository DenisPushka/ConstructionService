using DataAccess.Interface;
using DataAccess.models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthenticationRepository _authenticationRepository;
    
    public AuthorizationController(IAuthenticationRepository authenticationRepository)
    {
        _authenticationRepository = authenticationRepository;
    }

    // Аунтефикация
    [HttpPost("authentication")]
    public async Task<char> CheckAuthentication(UserAuthentication userAuthentication)
    {
        return await _authenticationRepository.Authentication(userAuthentication);
    }
}