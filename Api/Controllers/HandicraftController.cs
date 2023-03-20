using DataAccess.Interface;
using DataAccess.models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class HandicraftController : ControllerBase
{
    private readonly IHandcraftRepository _handcraftRepository;

    public HandicraftController(IHandcraftRepository handcraftRepository) => _handcraftRepository = handcraftRepository;

    [HttpPost("GetHandicraft")]
    public async Task<Handcraft> GetHandicraft(UserAuthentication user)
    {
        return await _handcraftRepository.GetHandicraft(user);
    }
}