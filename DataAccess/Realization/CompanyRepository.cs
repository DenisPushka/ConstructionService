using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Realization;

public class CompanyRepository : ICompanyRepository
{
    private readonly DataSqlCompany _sqlCompany;
    private readonly DataSqlFeedBack _sqlFeedBack;
    private readonly DataSqlService _sqlService;
    
    public CompanyRepository(DataSqlCompany sqlCompany, DataSqlFeedBack sqlFeedBack, DataSqlService sqlService)
    {
        _sqlCompany = sqlCompany;
        _sqlFeedBack = sqlFeedBack;
        _sqlService = sqlService;
    }
    
    public async Task<Company> GetCompany(UserAuthentication company)
    {
        return await _sqlCompany.Get(company);
    }

    public async Task<Company> GetAllInfoCompany(Company company)
    {
        return await _sqlCompany.GetAllInfoAboutCompany(company);
    }

    public async Task<Company[]> GetAllCompany()
    {
        return await _sqlCompany.GetAllCompany();
    }

    public async Task<Company[]> GetCompanyFromCity(string city)
    {
        return await _sqlCompany.GetCompanyFromCity(city);
    }

    public async Task<Feedback[]> GetFeedbacks(Company company)
    {
        return await _sqlCompany.GetFeedbacks(company);
    }

    public async Task<Work[]> GetWorks(UserAuthentication company)
    {
        return await _sqlService.GetWorksCompany(company);
    }

    public async Task<Equipment[]> GetEquipments(UserAuthentication company)
    {
        return await _sqlService.GetEquipmentsCompany(company);
    }

    public async Task<int> GetOrdersTaken(UserAuthentication user)
    {
        return await _sqlCompany.GetOrdersTaken(user);
    }

    public async Task<Order[]> GetOrders(UserAuthentication user)
    {
        return await _sqlCompany.GetOrders(user);
    }

    public async Task<Company[]> GetCompanyWithEquipment(int equipmentId)
    {
        return await _sqlCompany.GetCompanyWithEquipment(equipmentId);
    }

    public async Task<Company> AddCompany(Company company)
    {
        return await _sqlCompany.Add(company);
    }

    public async Task<Company> UpdateInfoCompany(Company company)
    {
        return await _sqlCompany.UpdateInfoCompany(company);
    }

    public async Task<Company> UpdateRating(Company company)
    {
        return await _sqlCompany.UpdateRating(company);
    }

    public async Task<Company> UpdateSubscription(Company company)
    {
        return await _sqlCompany.UpdateSubscription(company);
    }

    public async Task TakeOrder(UserAuthentication company, int orderId)
    {
        await _sqlCompany.TakeOrder(company, orderId);
    }

    public async Task RemoveOrder(int orderId)
    {
        await _sqlCompany.RemoveOrder(orderId);
    }

    public async Task<bool> PushMailToCustomer(Feedback feedback)
    {
        return await _sqlFeedBack.SentToUser(feedback);
    }

    public async Task TakeWork(Work work, Company company, Handcraft handcraft)
    {
        await _sqlService.TakeWork(work, company, handcraft);
    }

    public async Task TakeEquipment(Equipment equipment, Company company, Handcraft handcraft)
    {
        await _sqlService.TakeEquipment(equipment, company, handcraft);
    }
}