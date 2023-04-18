using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Interface;

public interface ICompanyRepository
{
    Task<Company> GetCompany(UserAuthentication userAuthentication);
    Task<Company> GetAllInfoCompany(Company company);
    Task<Company[]> GetAllCompany();
    Task<Company[]> GetCompanyFromCity(string city);
    Task<Feedback[]> GetFeedbacks(Company company);
    Task<Work[]> GetWorks(UserAuthentication company);
    Task<Equipment[]> GetEquipments(UserAuthentication company);

    Task<Company> AddCompany(Company company);
    Task<Company> UpdateInfoCompany(Company company);
    Task<Company> UpdateRating(Company company);
    Task<Company> UpdateSubscription(Company company);

    Task TakeOrder(UserAuthentication company, int orderId);
    Task RemoveOrder(int orderId);
    Task<bool> PushMailToCustomer(Feedback feedback);

    Task TakeWork(Work work, Company company, Handcraft handcraft);
    Task TakeEquipment(Equipment equipment, Company company, Handcraft handcraft);
}