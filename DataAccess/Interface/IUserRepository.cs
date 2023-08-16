using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess.Interface;

/// <summary>
/// Интерфейс репозиторя пользователя.
/// </summary>
public interface IUserRepository
{
    // Получение

    /// <summary>
    /// Получение пользвателя.
    /// </summary>
    /// <param name="user">Пользователь для авторизации.</param>
    /// <returns>Пользователь, при успешной авторизации.</returns>
    Task<User> GetUser(UserAuthentication user);

    /// <summary>
    /// Получение пользователей.
    /// </summary>
    /// <returns>Массив пользователей.</returns>
    Task<User[]> GetUsers();

    /// <summary>
    /// Получение пользователей из <paramref name="city"/>.
    /// </summary>
    /// <param name="city">Город, в котором ведется поиск.</param>
    /// <returns>Пользователи.</returns>
    Task<User[]> GetUserWhereSearchCity(string city);

    /// <summary>
    /// Получение заказа.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    /// <returns>Заказ.</returns>
    Task<Order> GetOrder(int orderId);

    /// <summary>
    /// Получение заказов.
    /// </summary>
    /// <returns>Массив заказов.</returns>
    Task<Order[]> GetOrders();

    /// <summary>
    /// Получение заказов у <paramref name="user"/>.
    /// </summary>
    /// <param name="user">Пользователь для авторизации.</param>
    /// <returns>Заказы у пользователя.</returns>
    Task<Order[]> ReceivingOrders(UserAuthentication user);

    /// <summary>
    /// Получение пользователя по заказу.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    /// <returns>Пользователь, которому принадлежит заказ.</returns>
    Task<User> GetUserWithOrder(int orderId);

    //------------------------------------------------------------------------------------------------------------------
    // Добавление

    /// <summary>
    /// Добавление пользователя.
    /// </summary>
    /// <param name="user">Добавляемый пользователь.</param>
    /// <returns>Добавленный пользователь.</returns>
    Task<User> AddUser(User user);

    /// <summary>
    /// Добавление заказа.
    /// </summary>
    /// <param name="order">Заказ.</param>
    /// <param name="user">Пользователь, к которому добавляется заказ.</param>
    /// <returns>Добавленный заказ.</returns>
    Task<Order> AddOrder(Order order, UserAuthentication user);

    /// <summary>
    /// Добавление отзыва пользователю.
    /// </summary>
    /// <param name="feedback">Отзыв.</param>
    Task AddFeedbackToEmployer(Feedback feedback);

    //------------------------------------------------------------------------------------------------------------------
    // Обновление

    /// <summary>
    /// Изменение пользователя.
    /// </summary>
    /// <param name="user">Изменный пользователь.</param>
    /// <returns>Изменный пользователь.</returns>
    Task<User> UpdateUser(User user);

    /// <summary>
    /// Обновление заказа.
    /// </summary>
    /// <param name="order">Обвленный заказ.</param>
    /// <returns>Обвленный заказ.</returns>
    Task<Order> UpdateOrder(Order order);

    /// <summary>
    /// Оставление отзыва.
    /// </summary>
    /// <param name="feedback">Отзыв.</param>
    /// <returns>true - в случае успеха.</returns>
    Task<bool> PushMailToExecutor(Feedback feedback);
}