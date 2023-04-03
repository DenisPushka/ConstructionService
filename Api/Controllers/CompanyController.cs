using DataAccess.Interface;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IServiceRepository _serviceRepository;

    public CompanyController(ICompanyRepository companyRepository, IServiceRepository serviceRepository)
    {
        _companyRepository = companyRepository;
        _serviceRepository = serviceRepository;
    }

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

    [HttpGet("GetFromCity/\'{city}\'")]
    public async Task<Company[]> GetCompanyFromCity([FromRoute] string city)
    {
        return await _companyRepository.GetCompanyFromCity(city);
    }

    [HttpPost("GetFeedback")]
    public async Task<Feedback[]> GetFeedbacks(Company company)
    {
        return await _companyRepository.GetFeedbacks(company);
    }
    
    // ДОБАВЛЕНИЕ
    [HttpPost("AddCompany")]
    public async Task<Company> AddCompany(Company company)
    {
        return await _companyRepository.AddCompany(company);
    }
    
    [HttpPost("addWork/\'{login}\' \'{password}\'")]
    public async Task<Work> AddWork([FromRoute]string login, [FromRoute] string password, Work work)
    {
        var company = await _companyRepository.GetCompany(new Company { Email = login, Password = password });
        return await _serviceRepository.AddWork(work, company.Id, 0);
    }
    
    [HttpPost("addEquipment/\'{login}\' \'{password}\'")]
    public async Task<Equipment> AddEquipment([FromRoute]string login, [FromRoute] string password,Equipment equipment)
    {
        var company = await _companyRepository.GetCompany(new Company { Email = login, Password = password });
        return await _serviceRepository.AddEquipment(equipment, company.Id, 0);
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

    // Связь с заказом
    [HttpPost("TakeOrder/{orderId:int}")]
    public async Task TakeOrder([FromRoute] int orderId, Company company)
    {
        await _companyRepository.TakeOrder(company, orderId);
    }

    // TODO попробовать прокинуть запрос
    [HttpDelete("RemoveOrder/{orderId:int}")]
    public async Task RemoveOrder([FromRoute] int orderId, Company company )
    {
        var companyWithId = await _companyRepository.GetCompany(company);
        await _companyRepository.RemoveOrder(orderId, companyWithId.Id, 0);
    }
}