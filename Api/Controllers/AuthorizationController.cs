using Api.Models;
using DataAccess.Interface;
using DataAccess.models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICompanyRepository _companyRepository;
    // private readonly IHandcraftRepository _handcraftRepository;

    public AuthorizationController(IAuthenticationRepository authenticationRepository, IUserRepository userRepository, ICompanyRepository companyRepository)
    {
        _authenticationRepository = authenticationRepository;
        _userRepository = userRepository;
        _companyRepository = companyRepository;
        // _handcraftRepository = handcraftRepository;
    }

    // Аунтефикация
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

        // var handcraft = await _handcraftRepository.GetHandicraft(userAuthentication);
        // if (!handcraft.Email.Equals(""))
        // {
        //     uch.Handcraft = handcraft;
        //     return uch;
        // }

        return uch;
    }
}