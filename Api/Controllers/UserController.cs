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

    [HttpPost("GetUser")]
    public async Task<User> GetCustomer(UserAuthentication userAuthentication)
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
    public async Task<Order> GetOrder([FromRoute]int orderId)
    {
        return await _userRepository.GetOrder(orderId);
    }

    [HttpPost("ReceivingOrders")]
    public async Task<Order[]> ReceivingOrder(UserAuthentication user)
    {
        return await _userRepository.ReceivingOrders(user);
    }
    
    [HttpPost("Add")]
    public async Task<User> AddUser(User user)
    {
        return await _userRepository.AddUser(user);
    }

    public async Task AddFeedbackToEmployer(Feedback feedback)
    {
        await _userRepository.AddFeedbackToEmployer(feedback);
    }

    [HttpPost("UpdateUser")]
    public async Task<User> ChangeUser(User user)
    {
        return await _userRepository.UpdateUser(user);
    }

    [HttpPost("AddOrder")]
    public async Task<Order> AddOrder([FromForm]Order order)
    {
        long length = order.Example.Length;
        if (length < 0)
            return new Order();

        using var fileStream = order.Example[0].OpenReadStream();
        byte[] bytes = new byte[length];
        fileStream.Read(bytes, 0, order.Example.Length);
        return await _userRepository.AddOrder(order);
    }

    [HttpPost("UpdateOrder")]
    public async Task<Order> ChangeOrder(Order order)
    {
        return await _userRepository.UpdateOrder(order);
    }

    [HttpPost("Feedback")]
    public async Task<bool> PushMailToExecutor(Feedback feedback)
    {
        return await _userRepository.PushMailToExecutor(feedback);
    }
}