using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess.Realization;

public class UserRepository : IUserRepository
{
    private readonly DataSqlUser _context;
    private readonly DataSqlFeedBack _feedBack;
    public UserRepository(DataSqlUser context, DataSqlFeedBack feedBack)
    {
        _context = context;
        _feedBack = feedBack;
    }
    
    public async Task<User> GetUser(UserAuthentication user)
    {
        return await _context.Get(user);
    }

    public async Task<User[]> GetUsers()
    {
        return await _context.GetUsers();
    }

    public async Task<User[]> GetUserWhereSearchCity(string city)
    {
        return await _context.GetUsersFromCity(city);
    }

    public async Task<Order> GetOrder(int orderId)
    {
        return await _context.GetOrder(orderId);
    }

    public async Task<Order[]> ReceivingOrders(UserAuthentication user)
    {
        return await _context.ReceivingOrders(user);
    }

    public async Task<User> AddUser(User user)
    {
        return await _context.Add(user);
    }

    public async Task<Order> AddOrder(Order order)
    {
        return await _context.AddOrder(order);
    }

    public async Task<User> UpdateUser(User user)
    {
        return await _context.Update(user);
    }

    public async Task<Order> UpdateOrder(Order order)
    {
        return await _context.UpdateOrder(order);
    }

    public async Task<bool> PushMailToExecutor(Feedback feedback)
    {
        return await _feedBack.SendToContactor(feedback);
    }

}