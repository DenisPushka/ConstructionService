using DataAccess.Interface;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess.Realization;

public class CompanyRepository : ICompanyRepository
{
    private readonly DataSqlCompany _sql;
    private DataSqlFeedBack _feedBack;
    
    public CompanyRepository(DataSqlCompany sql, DataSqlFeedBack feedBack)
    {
        _sql = sql;
        _feedBack = feedBack;
    }

    public async Task<Company> GetCompany(Company company)
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

    public async Task TakeOrder(Company company, int orderId)
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
}