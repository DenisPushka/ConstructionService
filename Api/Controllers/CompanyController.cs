using System.Threading.Tasks;
using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers;

/// <summary>
/// Контроллер компании.
/// </summary>
[ApiController, Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    /// <summary>
    /// Репозиторий компании.
    /// </summary>
    private readonly ICompanyRepository _companyRepository;

    /// <summary>
    /// Репозиторий сервиса.
    /// </summary>
    private readonly IServiceRepository _serviceRepository;

    /// <summary>
    /// Конструктор с двумя параметрами.
    /// </summary>
    public CompanyController(ICompanyRepository companyRepository, IServiceRepository serviceRepository)
    {
        _companyRepository = companyRepository;
        _serviceRepository = serviceRepository;
    }

    #region Get

    /// <summary>
    /// Получение компании.
    /// </summary>
    /// <param name="user">Пользователь для авторизации.</param>
    /// <returns>Компания.</returns>
    [HttpPost("GetCompany")]
    public async Task<Company> GetCompany([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetCompany(user);
    }

    /// <summary>
    /// Получение всех компаний.
    /// </summary>
    /// <returns>Массив компаний.</returns>
    [HttpPost("GetAll")]
    public async Task<Company[]> GetAllCompany()
    {
        return await _companyRepository.GetAllCompany();
    }

    /// <summary>
    /// Получение всей информации о компании.
    /// </summary>
    /// <param name="company">Компания о которой нужно получить всю информацию.</param>
    /// <returns>Компания со всеми данными.</returns>
    [HttpPost("GetAllInfoAboutCompany")]
    public async Task<Company> GetAllInfoAboutCompany([FromForm] Company company)
    {
        return await _companyRepository.GetAllInfoCompany(company);
    }

    /// <summary>
    /// Получение компаний по <paramref name="city"/>.
    /// </summary>
    /// <param name="city">Город, по которому ищутся компании.</param>
    /// <returns>Массив городов.</returns>
    [HttpGet("GetFromCity/\'{city}\'")]
    public async Task<Company[]> GetCompanyFromCity([FromRoute] string city)
    {
        return await _companyRepository.GetCompanyFromCity(city);
    }

    /// <summary>
    /// Получение отзыва.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив отзывов.</returns>
    [HttpPost("GetFeedback")]
    public async Task<Feedback[]> GetFeedbacks([FromForm] Company company)
    {
        return await _companyRepository.GetFeedbacks(company);
    }

    /// <summary>
    /// Получение работ, которые предоставляет компания.
    /// </summary>
    /// <param name="user">Пользователь авторизации.</param>
    /// <returns>Массив работ.</returns>
    [HttpPost("GetWorks")]
    public async Task<Work[]> GetWorks([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetWorks(user);
    }
    
    /// <summary>
    /// Получение оборудования, которое предоставляет компания.
    /// </summary>
    /// <param name="user">Пользователь авторизации.</param>
    /// <returns>Массив оборудований.</returns>
    [HttpPost("GetEquipments")]
    public async Task<Equipment[]> GetEquipments([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetEquipments(user);
    }

    /// <summary>
    /// Получение выполненных заказов у данной компании.
    /// </summary>
    /// <param name="user">Пользователь авторизации.</param>
    /// <returns>Количество заказов.</returns>
    [HttpPost("GetOrdersTaken")]
    public async Task<int> GetOrdersTaken([FromForm] UserAuthentication user)
    {
        return await _companyRepository.GetOrdersTaken(user);
    }

    /// <summary>
    /// Получение заказов.
    /// </summary>
    /// <param name="company">Компания у котрой ищутся заказы.</param>
    /// <returns>Массив заказов.</returns>
    [HttpPost("GetOrders")]
    public async Task<Order[]> GetOrders([FromForm] UserAuthentication company)
    {
        return await _companyRepository.GetOrders(company);
    }

    /// <summary>
    /// Получение компаний по определенному оборудованию.
    /// </summary>
    /// <param name="equipmentId">Id оборудования.</param>
    /// <returns>Массив компаний.</returns>
    [HttpGet("CompanyWithEquipment/{equipmentId:int}")]
    public async Task<Company[]> GetCompanyWithEquipment([FromRoute] int equipmentId)
    {
        return await _companyRepository.GetCompanyWithEquipment(equipmentId);
    }

    #endregion

    #region Add

    /// <summary>
    /// Добавление компании.
    /// </summary>
    /// <param name="company">Добавляемая компания.</param>
    /// <returns>Добавляемая компания.</returns>
    [HttpPost("AddCompany")]
    public async Task<Company> AddCompany([FromForm] Company company)
    {
        return await _companyRepository.AddCompany(company);
    }

    /// <summary>
    /// Добавление работы компании.
    /// </summary>
    /// <param name="form">Форма, в которой имеется объект для авторизации и работа.</param>
    /// <returns>Работа, которая была добавлена в компанию.</returns>
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

    /// <summary>
    /// Добавление оборудования компании.
    /// </summary>
    /// <param name="form">Форма, в которой имеется объект для авторизации и оборудование.</param>
    /// <returns>Оборудования, которая было добавлено в компанию.</returns>
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

    /// <summary>
    /// Обновление данных у компании.
    /// </summary>
    /// <param name="company">Данные с обновленными данными.</param>
    /// <returns>Компания с обновленными данными.</returns>
    [HttpPost("UpdateCompany")]
    public async Task<Company> UpdateInfo([FromForm] Company company)
    {
        return await _companyRepository.UpdateInfoCompany(company);
    }

    /// <summary>
    /// Обновление рейтинга.
    /// </summary>
    /// <param name="company">Компания, у которой обновляется рейтинг.</param>
    /// <returns>Компания, у которой обновляется рейтинг.</returns>
    [HttpPost("UpdateRating")]
    public async Task<Company> UpdateRating([FromForm] Company company)
    {
        return await _companyRepository.UpdateRating(company);
    }

    [HttpPost("UpdateSubstring")]
    public async Task<Company> UpdateSubstring([FromForm] Company company)
    {
        return await _companyRepository.UpdateSubscription(company);
    }

    #endregion

    /// <summary>
    /// Добавление (из имеющихся) работы у компании.
    /// </summary>
    /// <param name="form">Хранит в себе пользователя для авторизации и работу.</param>
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

    /// <summary>
    /// Добавление (из имеющегося) оборудования у компании.
    /// </summary>
    /// <param name="form">Хранит в себе пользователя для авторизации и оборудования.</param>
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

    /// <summary>
    /// Взятие заказа.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    /// <param name="company">Компания, которая берет заказ.</param>
    [HttpPost("TakeOrder/{orderId:int}")]
    public async Task TakeOrder([FromRoute] int orderId, [FromForm] UserAuthentication company)
    {
        await _companyRepository.TakeOrder(company, orderId);
    }

    /// <summary>
    /// Удаление (отказ) от заказа.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    [HttpDelete("RemoveOrder/{orderId:int}")]
    public async Task RemoveOrder([FromRoute] int orderId)
    {
        await _companyRepository.RemoveOrder(orderId);
    }
}