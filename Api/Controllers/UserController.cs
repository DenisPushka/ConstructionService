using System.Threading.Tasks;
using Api.Models;
using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для пользователя.
/// </summary>
[ApiController, Route("api/[controller]")]
public class UserController : ControllerBase
{
    /// <summary>
    /// Репозиторий для пользователя.
    /// </summary>
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) => _userRepository = userRepository;

    #region Get

    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <param name="userAuthentication">Пользователь для авторизации.</param>
    /// <returns>Пользователь.</returns>
    [HttpPost("GetUser")]
    public async Task<User> GetCustomer([FromForm] UserAuthentication userAuthentication)
    {
        return await _userRepository.GetUser(userAuthentication);
    }

    /// <summary>
    /// Получение пользователей.
    /// </summary>
    /// <returns>Массив пользователей.</returns>
    [HttpGet("GetUsers")]
    public async Task<User[]> GetUsers() => await _userRepository.GetUsers();

    /// <summary>
    /// Получение пользователей из <paramref name="city"/>.
    /// </summary>
    /// <param name="city">Город, в котором происходит поиск.</param>
    /// <returns>Массив пользователей.</returns>
    [HttpGet("City/\'{city}\'")]
    public async Task<User[]> GetUserWhereSearchCity([FromRoute] string city)
    {
        return await _userRepository.GetUserWhereSearchCity(city);
    }

    /// <summary>
    /// Получение заказа.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    /// <returns>Заказ.</returns>
    [HttpGet("GetOrder/{orderId:int}")]
    public async Task<Order> GetOrder([FromRoute] int orderId)
    {
        return await _userRepository.GetOrder(orderId);
    }

    /// <summary>
    /// Получение заказов.
    /// </summary>
    /// <returns>Массив заказов.</returns>
    [HttpGet("getOrders")]
    public async Task<Order[]> GetOrders()
    {
        return await _userRepository.GetOrders();
    }

    /// <summary>
    /// Получение заказов у <paramref name="user"/>.
    /// </summary>
    /// <param name="user">Пользователь для авторизации.</param>
    /// <returns>Заказы у пользователя.</returns>
    [HttpPost("ReceivingOrders")]
    public async Task<Order[]> ReceivingOrder([FromForm] UserAuthentication user)
    {
        return await _userRepository.ReceivingOrders(user);
    }

    /// <summary>
    /// Получение пользователя по заказу.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    /// <returns>Пользователь, которому принадлежит заказ.</returns>
    [HttpGet("GetUserWithOrder/{orderId:int}")]
    public async Task<User> GetUserWithOrder([FromRoute] int orderId)
    {
        return await _userRepository.GetUserWithOrder(orderId);
    }

    #endregion

    #region Add

    /// <summary>
    /// Добавление пользователя.
    /// </summary>
    /// <param name="user">Добавляемый пользователь.</param>
    /// <returns>Добавленный пользователь.</returns>
    [HttpPost("Add")]
    public async Task<User> AddUser([FromForm] User user)
    {
        return await _userRepository.AddUser(user);
    }

    /// <summary>
    /// Добавление заказа.
    /// </summary>
    /// <param name="orderFromVie">Заказ.</param>
    /// <returns>Добавленный заказ.</returns>
    [HttpPost("AddOrder")]
    public async Task<Order> AddOrder([FromForm] OrderFromVie orderFromVie)
    {
        var user = orderFromVie.ToUserAuthentication().Result;
        return await _userRepository.AddOrder(await orderFromVie.OrderVieToOrder(), user);
    }
    
    /// <summary>
    /// Добавление отзыва пользователю.
    /// </summary>
    /// <param name="feedback">Отзыв.</param>
    public async Task AddFeedbackToEmployer([FromForm] Feedback feedback)
    {
        await _userRepository.AddFeedbackToEmployer(feedback);
    }

    #endregion

    /// <summary>
    /// Изменение пользователя.
    /// </summary>
    /// <param name="user">Изменный пользователь.</param>
    /// <returns>Изменный пользователь.</returns>
    [HttpPost("UpdateUser")]
    public async Task<User> ChangeUser([FromForm] User user)
    {
        if (user.Email.Equals(""))
            return new User();
        return await _userRepository.UpdateUser(user);
    }

    /// <summary>
    /// Обновление заказа.
    /// </summary>
    /// <param name="order">Обвленный заказ.</param>
    /// <returns>Обвленный заказ.</returns>
    [HttpPost("UpdateOrder")]
    public async Task<Order> ChangeOrder([FromForm] Order order)
    {
        return await _userRepository.UpdateOrder(order);
    }

    /// <summary>
    /// Оставление отзыва.
    /// </summary>
    /// <param name="feedback">Отзыв.</param>
    /// <returns>true - в случае успеха.</returns>
    [HttpPost("Feedback")]
    public async Task<bool> PushMailToExecutor([FromForm] Feedback feedback)
    {
        return await _userRepository.PushMailToExecutor(feedback);
    }
}