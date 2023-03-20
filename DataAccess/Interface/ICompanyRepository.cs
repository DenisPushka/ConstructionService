using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess.Interface;

public interface ICompanyRepository
{
    Task<Company> GetCompany(Company company);
    Task<Company> GetAllInfoCompany(Company company);
    Task<Company[]> GetAllCompany();
    Task<Company[]> GetCompanyFromCity(string city);
    
    Task<Company> AddCompany(Company company);
    Task<Company> UpdateInfoCompany(Company company);
    Task<Company> UpdateRating(Company company);
    
    Task TakeOrder(Company company, int orderId);
    Task RemoveOrder(int orderId);
    Task<bool> PushMailToCustomer(Feedback feedback);
}