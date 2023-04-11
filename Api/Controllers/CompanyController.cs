using Api.Models;
using DataAccess.Interface;
using DataAccess.models;
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
    public async Task<Company> GetCompany([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetCompany(user);
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
    public async Task<Company> AddCompany([FromForm] Company company)
    {
        return await _companyRepository.AddCompany(company);
    }

    [HttpPost("addWork")]
    public async Task<Work> AddWork([FromForm] AddWorkCompany addWorkCompany)
    {
        var company = await _companyRepository.GetCompany(addWorkCompany.UserAuthentication);
        return await _serviceRepository.AddWork(addWorkCompany.Work, company.Id, 0);
    }

    [HttpPost("addEquipment")]
    public async Task<Equipment> AddEquipment([FromForm] AddEquipmentCompany addEquipmentCompany)
    {
        var company = await _companyRepository.GetCompany(addEquipmentCompany.UserAuthentication);
        return await _serviceRepository.AddEquipment(addEquipmentCompany.Equipment, company.Id, 0);
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
    public async Task RemoveOrder([FromRoute] int orderId, UserAuthentication userAuthentication)
    {
        var companyWithId = await _companyRepository.GetCompany(userAuthentication);
        await _companyRepository.RemoveOrder(orderId, companyWithId.Id, 0);
    }
}