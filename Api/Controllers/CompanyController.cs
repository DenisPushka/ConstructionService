using Api.Models;
using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    
    [HttpPost("GetWorks")]
    public async Task<Work[]>GetWorks([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetWorks(user);
    }
    
    // [HttpPost("GetWorks")]
    // public async Task<Work[]>GetWorks([FromForm] UserAuthentication user)
    // {
    //     return await _companyRepository.GetWorks(user);
    // }
    
    // ДОБАВЛЕНИЕ
    [HttpPost("AddCompany")]
    public async Task<Company> AddCompany([FromForm] Company company)
    {
        return await _companyRepository.AddCompany(company);
    }

    [HttpPost("addWork")]
    public async Task<Work> AddWork(IFormCollection form)
    {
        var userJson = form["UserAuthentication"];
        var workJson = form["Work"];
        var user = JsonConvert.DeserializeObject<UserAuthentication>(userJson);
        var company = await _companyRepository.GetCompany(user);
        if (company.Name.Equals(""))
        {
            return new Work();
        }
        
        var work = JsonConvert.DeserializeObject<Work>(workJson);
        return await _serviceRepository.AddWork(work, company, new Handcraft());
    }

    [HttpPost("addEquipment")]
    public async Task<Equipment> AddEquipment(IFormCollection form)
    {
        var userJson = form["UserAuthentication"];
        var equipmentJson = form["Equipment"];
        var user = JsonConvert.DeserializeObject<UserAuthentication>(userJson);
        var company = await _companyRepository.GetCompany(user);
        if (company.Name.Equals(""))
        {
            return new Equipment();
        }
        var equipment = JsonConvert.DeserializeObject<Equipment>(equipmentJson);
        return await _serviceRepository.AddEquipment(equipment, company, new Handcraft());
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

    [HttpPost("TakeWork")]
    public async Task TakeWork(IFormCollection form)
    {
        var userJson = form["UserAuthentication"];
        var workJson = form["Work"];
        var user = JsonConvert.DeserializeObject<UserAuthentication>(userJson);
        var company = await _companyRepository.GetCompany(user);
        if (company.Name.Equals(""))
        {
            return;
        }

        var work = JsonConvert.DeserializeObject<Work>(workJson);
        await _companyRepository.TakeWork(work, company, new Handcraft());
    }
    
    // Связь с заказом
    [HttpPost("TakeOrder/{orderId:int}")]
    public async Task TakeOrder([FromRoute] int orderId, UserAuthentication company)
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