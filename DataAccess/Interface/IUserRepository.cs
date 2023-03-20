using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess.Interface;

public interface IUserRepository
{
    // Получение
    Task<User> GetUser(UserAuthentication user);
    Task<User[]> GetUsers();
    Task<User[]> GetUserWhereSearchCity(string city);
    Task<Order> GetOrder(int orderId);
    Task<Order[]> ReceivingOrders(UserAuthentication user);

    // Добавление
    Task<User> AddUser(User user);
    Task<Order> AddOrder(Order order);

    // Обновление
    Task<User> UpdateUser(User user);
    Task<Order> UpdateOrder(Order order);
    Task<bool> PushMailToExecutor(Feedback feedback);
}