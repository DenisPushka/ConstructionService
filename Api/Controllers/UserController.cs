using Api.Models;
using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) => _userRepository = userRepository;

    #region Get

    [HttpPost("GetUser")]
    public async Task<User> GetCustomer([FromForm] UserAuthentication userAuthentication)
    {
        return await _userRepository.GetUser(userAuthentication);
    }

    [HttpGet("GetUsers")]
    public async Task<User[]> GetUsers() => await _userRepository.GetUsers();

    [HttpGet("City/\'{city}\'")]
    public async Task<User[]> GetUserWhereSearchCity([FromRoute] string city)
    {
        return await _userRepository.GetUserWhereSearchCity(city);
    }

    [HttpGet("GetOrder/{orderId:int}")]
    public async Task<Order> GetOrder([FromRoute] int orderId)
    {
        return await _userRepository.GetOrder(orderId);
    }

    [HttpGet("getOrders")]
    public async Task<Order[]> GetOrders()
    {
        return await _userRepository.GetOrders();
    }

    [HttpPost("ReceivingOrders")]
    public async Task<Order[]> ReceivingOrder([FromForm] UserAuthentication user)
    {
        return await _userRepository.ReceivingOrders(user);
    }

    [HttpGet("GetUserWithOrder/{orderId:int}")]
    public async Task<User> GetUserWithOrder([FromRoute] int orderId)
    {
        return await _userRepository.GetUserWithOrder(orderId);
    }

    #endregion

    #region Add

    [HttpPost("Add")]
    public async Task<User> AddUser([FromForm] User user)
    {
        return await _userRepository.AddUser(user);
    }

    [HttpPost("AddOrder")]
    public async Task<Order> AddOrder([FromForm] OrderFromVie orderFromVie)
    {
        var user = orderFromVie.ToUserAuthentication().Result;
        return await _userRepository.AddOrder(await orderFromVie.OrderVieToOrder(), user);
    }

    public async Task AddFeedbackToEmployer([FromForm] Feedback feedback)
    {
        await _userRepository.AddFeedbackToEmployer(feedback);
    }

    #endregion

    [HttpPost("UpdateUser")]
    public async Task<User> ChangeUser([FromForm] User user)
    {
        if (user.Email.Equals(""))
            return new User();
        return await _userRepository.UpdateUser(user);
    }

    [HttpPost("UpdateOrder")]
    public async Task<Order> ChangeOrder([FromForm] Order order)
    {
        return await _userRepository.UpdateOrder(order);
    }

    [HttpPost("Feedback")]
    public async Task<bool> PushMailToExecutor([FromForm] Feedback feedback)
    {
        return await _userRepository.PushMailToExecutor(feedback);
    }
}