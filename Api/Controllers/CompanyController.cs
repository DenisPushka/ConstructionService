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

    #region Get

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
    public async Task<Company> GetAllInfoAboutCompany([FromForm] Company company)
    {
        return await _companyRepository.GetAllInfoCompany(company);
    }

    [HttpGet("GetFromCity/\'{city}\'")]
    public async Task<Company[]> GetCompanyFromCity([FromRoute] string city)
    {
        return await _companyRepository.GetCompanyFromCity(city);
    }

    [HttpPost("GetFeedback")]
    public async Task<Feedback[]> GetFeedbacks([FromForm] Company company)
    {
        return await _companyRepository.GetFeedbacks(company);
    }

    [HttpPost("GetWorks")]
    public async Task<Work[]> GetWorks([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetWorks(user);
    }
    
    [HttpPost("GetEquipments")]
    public async Task<Equipment[]>GetEquipments([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetEquipments(user);
    }

    [HttpPost("GetOrdersTaken")]
    public async Task<int> GetOrdersTaken([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetOrdersTaken(user);
    }
    
    [HttpPost("GetOrders")]
    public async Task<Order[]> GetOrders([FromForm] UserAuthentication company)
    {
        return await _companyRepository.GetOrders(company);
    }

    [HttpGet("CompanyWithEquipment/{equipmentId:int}")]
    public async Task<Company[]> GetCompanyWithEquipment([FromRoute]int equipmentId)
    {
        return await _companyRepository.GetCompanyWithEquipment(equipmentId);
    }
    
    #endregion

    #region Add

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

    #endregion

    #region Update

    [HttpPost("UpdateCompany")]
    public async Task<Company> UpdateInfo([FromForm]Company company)
    {
        return await _companyRepository.UpdateInfoCompany(company);
    }

    [HttpPost("UpdateRating")]
    public async Task<Company> UpdateRating([FromForm]Company company)
    {
        return await _companyRepository.UpdateRating(company);
    }

    [HttpPost("UpdateSubstring")]
    public async Task<Company> UpdateSubstring([FromForm]Company company)
    {
        return await _companyRepository.UpdateSubscription(company);
    }

    #endregion

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
    
    [HttpPost("TakeEquipment")]
    public async Task TakeEquipment(IFormCollection form)
    {
        var userJson = form["UserAuthentication"];
        var equipmentJson = form["Equipment"];
        var user = JsonConvert.DeserializeObject<UserAuthentication>(userJson);
        var company = await _companyRepository.GetCompany(user);
        if (company.Name.Equals(""))
        {
            return;
        }

        var equipment = JsonConvert.DeserializeObject<Equipment>(equipmentJson);
        await _companyRepository.TakeEquipment(equipment, company, new Handcraft());
    }

    [HttpPost("TakeOrder/{orderId:int}")]
    public async Task TakeOrder([FromRoute] int orderId, [FromForm] UserAuthentication company)
    {
        await _companyRepository.TakeOrder(company, orderId);
    }

    [HttpDelete("RemoveOrder/{orderId:int}")]
    public async Task RemoveOrder([FromRoute] int orderId)
    {
        await _companyRepository.RemoveOrder(orderId);
    }
}