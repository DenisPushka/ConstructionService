using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Realization;

public class CompanyRepository : ICompanyRepository
{
    private readonly DataSqlCompany _sql;
    private readonly DataSqlFeedBack _feedBack;
    private readonly DataSqlService _sqlService;
    
    public CompanyRepository(DataSqlCompany sql, DataSqlFeedBack feedBack, DataSqlService sqlService)
    {
        _sql = sql;
        _feedBack = feedBack;
        _sqlService = sqlService;
    }
    
    public async Task<Company> GetCompany(UserAuthentication company)
    {
        return await _sql.Get(company);
    }

    public async Task<Company> GetAllInfoCompany(Company company)
    {
        return await _sql.GetAllInfoAboutCompany(company);
    }

    public async Task<Company[]> GetAllCompany()
    {
        return await _sql.GetAllCompany();
    }

    public async Task<Company[]> GetCompanyFromCity(string city)
    {
        return await _sql.GetCompanyFromCity(city);
    }

    public async Task<Feedback[]> GetFeedbacks(Company company)
    {
        return await _sql.GetFeedbacks(company);
    }

    public async Task<Work[]> GetWorks(UserAuthentication company)
    {
        return await _sqlService.GetWorksCompany(company);
    }

    public async Task<Equipment[]> GetEquipments(UserAuthentication company)
    {
        return await _sqlService.GetEquipmentsCompany(company);
    }

    public async Task<Company> AddCompany(Company company)
    {
        return await _sql.Add(company);
    }

    public async Task<Company> UpdateInfoCompany(Company company)
    {
        return await _sql.UpdateInfoCompany(company);
    }

    public async Task<Company> UpdateRating(Company company)
    {
        return await _sql.UpdateRating(company);
    }

    public async Task TakeOrder(UserAuthentication company, int orderId)
    {
        await _sql.TakeOrder(company, orderId);
    }

    public async Task RemoveOrder(int orderId, int companyId, int handcraftId)
    {
        await _sql.RemoveOrder(orderId, companyId, handcraftId);
    }

    public async Task<bool> PushMailToCustomer(Feedback feedback)
    {
        return await _feedBack.SentToUser(feedback);
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