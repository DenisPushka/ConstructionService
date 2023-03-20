using DataAccess.Interface;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository) => _companyRepository = companyRepository;

    [HttpPost("GetCompany")]
    public async Task<Company> GetCompany(Company company)
    {
        return await _companyRepository.GetCompany(company);
    }

    [HttpPost("GetAll")]
    public async Task<Company[]> GetAllCompany()
    {
        return await _companyRepository.GetAllCompany();
    }

    [HttpPost("GetAllInfoAboutCompany")]
    public async Task<Company> GetAllInfoAboutCompany(Company company)
    {
        return await _companyRepository.GetAllInfoCompany(company);
    }

    [HttpGet("GetFromCity/{city}")]
    public async Task<Company[]> GetCompanyFromCity([FromRoute] string city)
    {
        return await _companyRepository.GetCompanyFromCity(city);
    }

    [HttpPost("AddCompany")]
    public async Task<Company> AddCompany(Company company)
    {
        return await _companyRepository.AddCompany(company);
    }

    [HttpPost("UpdateCompany")]
    public async Task<Company> UpdateInfo(Company company)
    {
        return await _companyRepository.UpdateInfoCompany(company);
    }

    [HttpPost("UpdateRating")]
    public async Task<Company> UpdateRating(Company company)
    {
        return await _companyRepository.UpdateRating(company);
    }

    [HttpPost("TakeOrder/{orderId:int}")]
    public async Task TakeOrder([FromRoute] int orderId, Company company)
    {
        await _companyRepository.TakeOrder(company, orderId);
    }

    [HttpDelete("RemoveOrder/{orderId:int}")]
    public async Task RemoveOrder([FromRoute] int orderId)
    {
        await _companyRepository.RemoveOrder(orderId);
    }
}