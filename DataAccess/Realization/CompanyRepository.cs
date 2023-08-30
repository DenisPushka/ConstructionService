using System.Threading.Tasks;
using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Realization;

/// <summary>
/// Репозиторий для компании.
/// </summary>
public class CompanyRepository : ICompanyRepository
{
    /// <summary>
    /// Реализатор для компании.
    /// </summary>
    private readonly DataSqlCompany _sqlCompany;

    /// <summary>
    /// Реалзитор для отзывов.
    /// </summary>
    private readonly DataSqlFeedBack _sqlFeedBack;

    /// <summary>
    /// Реализотор для сервиса.
    /// </summary>
    private readonly DataSqlService _sqlService;

    public CompanyRepository(DataSqlCompany sqlCompany, DataSqlFeedBack sqlFeedBack, DataSqlService sqlService)
    {
        _sqlCompany = sqlCompany;
        _sqlFeedBack = sqlFeedBack;
        _sqlService = sqlService;
    }

    /// <summary>
    /// Получение компании.
    /// </summary>
    /// <param name="company">Пользователь для авторизации.</param>
    /// <returns>Компания.</returns>
    public async Task<Company> GetCompany(UserAuthentication company)
    {
        return await _sqlCompany.Get(company);
    }

    /// <summary>
    /// Вся информация о <paramref name="company"/>.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Компания со всеми данными о ней.</returns>
    public async Task<Company> GetAllInfoCompany(Company company)
    {
        return await _sqlCompany.GetAllInfoAboutCompany(company);
    }

    /// <summary>
    /// Получение всех компаний.
    /// </summary>
    /// <returns>Массив компаний.</returns>
    public async Task<Company[]> GetAllCompany()
    {
        return await _sqlCompany.GetAllCompany();
    }

    /// <summary>
    /// Получние компаний из <paramref name="city"/>.
    /// </summary>
    /// <param name="city">Город, по которому идет поиск.</param>
    /// <returns>Массив компаний.</returns>
    public async Task<Company[]> GetCompanyFromCity(string city)
    {
        return await _sqlCompany.GetCompanyFromCity(city);
    }

    /// <summary>
    /// Получение отзывов о компании.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив отзывов.</returns>
    public async Task<Feedback[]> GetFeedbacks(Company company)
    {
        return await _sqlCompany.GetFeedbacks(company);
    }

    /// <summary>
    /// Работы, которые предлагает компания.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив работ.</returns>
    public async Task<Work[]> GetWorks(UserAuthentication company)
    {
        return await _sqlService.GetWorksCompany(company);
    }

    /// <summary>
    /// Оборудования, которые предлагает компания.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив оборудований.</returns>
    public async Task<Equipment[]> GetEquipments(UserAuthentication company)
    {
        return await _sqlService.GetEquipmentsCompany(company);
    }

    /// <summary>
    /// Получение выполненных заказов компанией.
    /// </summary>
    /// <param name="user">Пользователь для авторизици.</param>
    /// <returns>Количество выполненных заказов.</returns>
    public async Task<int> GetOrdersTaken(UserAuthentication user)
    {
        return await _sqlCompany.GetOrdersTaken(user);
    }

    /// <summary>
    /// Получние заказов.
    /// </summary>
    /// <param name="user">Пользователь для атворизации.</param>
    /// <returns>Массив заказов (у данного пользователя).</returns>
    public async Task<Order[]> GetOrders(UserAuthentication user)
    {
        return await _sqlCompany.GetOrders(user);
    }

    /// <summary>
    /// Получение компаний, которые имеют в аренду <paramref name="equipmentId"/>.
    /// </summary>
    /// <param name="equipmentId">Id оборудования.</param>
    /// <returns>Массив компаний.</returns>
    public async Task<Company[]> GetCompanyWithEquipment(int equipmentId)
    {
        return await _sqlCompany.GetCompanyWithEquipment(equipmentId);
    }

    /// <summary>
    /// Добавление компании.
    /// </summary>
    /// <param name="company">Добавляемая комания.</param>
    /// <returns>Добавленная компания.</returns>
    public async Task<Company> AddCompany(Company company)
    {
        return await _sqlCompany.Add(company);
    }

    /// <summary>
    /// Обновелние компании.
    /// </summary>
    /// <param name="company">Обновленная комания.</param>
    /// <returns>Обновленная комания.</returns>
    public async Task<Company> UpdateInfoCompany(Company company)
    {
        return await _sqlCompany.UpdateInfoCompany(company);
    }

    /// <summary>
    /// Измененение рейтинга.
    /// </summary>
    /// <param name="company">Комания с изменным рейтингом.</param>
    /// <returns>Комания с изменным рейтингом.</returns>
    public async Task<Company> UpdateRating(Company company)
    {
        return await _sqlCompany.UpdateRating(company);
    }

    /// <summary>
    /// Изменение подписки.
    /// </summary>
    /// <param name="company">Компания с измененной подпиской.</param>
    /// <returns>Компания с измененной подпиской.</returns>
    public async Task<Company> UpdateSubscription(Company company)
    {
        return await _sqlCompany.UpdateSubscription(company);
    }

    /// <summary>
    /// Взятие заказа.
    /// </summary>
    /// <param name="company">Компания, которая берет заказ.</param>
    /// <param name="orderId">Id заказа.</param>
    public async Task TakeOrder(UserAuthentication company, int orderId)
    {
        await _sqlCompany.TakeOrder(company, orderId);
    }

    /// <summary>
    /// Удаление (отказ) от заказа.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    public async Task RemoveOrder(int orderId)
    {
        await _sqlCompany.RemoveOrder(orderId);
    }

    public async Task<bool> PushMailToCustomer(Feedback feedback)
    {
        return await _sqlFeedBack.SentToUser(feedback);
    }

    /// <summary>
    /// Взятие работы.
    /// </summary>
    /// <param name="work">Работа.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    public async Task TakeWork(Work work, Company company, Handcraft handcraft)
    {
        await _sqlService.TakeWork(work, company, handcraft);
    }

    /// <summary>
    /// Взятие оборудования.
    /// </summary>
    /// <param name="equipment">Оборудование.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    public async Task TakeEquipment(Equipment equipment, Company company, Handcraft handcraft)
    {
        await _sqlService.TakeEquipment(equipment, company, handcraft);
    }
}