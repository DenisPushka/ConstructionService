using System.Threading.Tasks;
using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess.Realization;

/// <summary>
/// Репозиторий для пользователя.
/// </summary>
public class UserRepository : IUserRepository
{
    /// <summary>
    /// Реалзитор для пользователя.
    /// </summary>
    private readonly DataSqlUser _context;
    
    /// <summary>
    /// Реализатор для отзывов.
    /// </summary>
    private readonly DataSqlFeedBack _feedBack;

    public UserRepository(DataSqlUser context, DataSqlFeedBack feedBack)
    {
        _context = context;
        _feedBack = feedBack;
    }

    /// <summary>
    /// Получение пользователя.
    /// </summary>
    /// <param name="user">Пользователь для авторизации.</param>
    /// <returns>Пользователь.</returns>
    public async Task<User> GetUser(UserAuthentication user) => await _context.Get(user);

    /// <summary>
    /// Получение пользователей.
    /// </summary>
    /// <returns>Массив пользователей.</returns>
    public async Task<User[]> GetUsers() => await _context.GetUsers();

    /// <summary>
    /// Получение пользователей из <paramref name="city"/>.
    /// </summary>
    /// <param name="city">Город, в котором происходит поиск.</param>
    /// <returns>Массив пользователей.</returns>
    public async Task<User[]> GetUserWhereSearchCity(string city) => await _context.GetUsersFromCity(city);

    /// <summary>
    /// Получение заказа.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    /// <returns>Заказ.</returns>
    public async Task<Order> GetOrder(int orderId) => await _context.GetOrder(orderId);

    /// <summary>
    /// Получение заказов.
    /// </summary>
    /// <returns>Массив заказов.</returns>
    public async Task<Order[]> GetOrders() => await _context.GetOrders();

    /// <summary>
    /// Получение заказов у <paramref name="user"/>.
    /// </summary>
    /// <param name="user">Пользователь для авторизации.</param>
    /// <returns>Заказы у пользователя.</returns>
    public async Task<Order[]> ReceivingOrders(UserAuthentication user) => await _context.ReceivingOrders(user);

    /// <summary>
    /// Получение пользователя по заказу.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    /// <returns>Пользователь, которому принадлежит заказ.</returns>
    public async Task<User> GetUserWithOrder(int orderId) => await _context.GetUserWithOrder(orderId);

    /// <summary>
    /// Добавление пользователя.
    /// </summary>
    /// <param name="user">Добавляемый пользователь.</param>
    /// <returns>Добавленный пользователь.</returns>
    public async Task<User> AddUser(User user) => await _context.Add(user);

    /// <summary>
    /// Добавление заказа.
    /// </summary>
    /// <param name="order">Заказ.</param>
    /// <param name="user">Пользователь, к которому добавляется заказ.</param>
    /// <returns>Добавленный заказ.</returns>
    public async Task<Order> AddOrder(Order order, UserAuthentication user) => await _context.AddOrder(order, user);

    /// <summary>
    /// Добавление отзыва пользователю.
    /// </summary>
    /// <param name="feedback">Отзыв.</param>
    public async Task AddFeedbackToEmployer(Feedback feedback) => await _context.AddFeedbackToEmployer(feedback);

    /// <summary>
    /// Изменение пользователя.
    /// </summary>
    /// <param name="user">Изменный пользователь.</param>
    /// <returns>Изменный пользователь.</returns>
    public async Task<User> UpdateUser(User user) => await _context.Update(user);

    /// <summary>
    /// Обновление заказа.
    /// </summary>
    /// <param name="order">Обвленный заказ.</param>
    /// <returns>Обвленный заказ.</returns>
    public async Task<Order> UpdateOrder(Order order) => await _context.UpdateOrder(order);

    /// <summary>
    /// Оставление отзыва.
    /// </summary>
    /// <param name="feedback">Отзыв.</param>
    /// <returns>true - в случае успеха.</returns>
    public async Task<bool> PushMailToExecutor(Feedback feedback) => await _feedBack.SendToContactor(feedback);
}