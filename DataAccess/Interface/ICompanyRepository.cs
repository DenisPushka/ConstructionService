using System.Threading.Tasks;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Interface;

/// <summary>
/// Интерфейс репозитория компании.
/// </summary>
public interface ICompanyRepository
{
    /// <summary>
    /// Получение компании.
    /// </summary>
    /// <param name="userAuthentication">Пользователь для авторизации.</param>
    /// <returns>Компания.</returns>
    Task<Company> GetCompany(UserAuthentication userAuthentication);

    /// <summary>
    /// Вся информация о <paramref name="company"/>.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Компания со всеми данными о ней.</returns>
    Task<Company> GetAllInfoCompany(Company company);

    /// <summary>
    /// Получение всех компаний.
    /// </summary>
    /// <returns>Массив компаний.</returns>
    Task<Company[]> GetAllCompany();

    /// <summary>
    /// Получние компаний из <paramref name="city"/>.
    /// </summary>
    /// <param name="city">Город, по которому идет поиск.</param>
    /// <returns>Массив компаний.</returns>
    Task<Company[]> GetCompanyFromCity(string city);

    /// <summary>
    /// Получение отзывов о компании.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив отзывов.</returns>
    Task<Feedback[]> GetFeedbacks(Company company);

    /// <summary>
    /// Работы, которые предлагает компания.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив работ.</returns>
    Task<Work[]> GetWorks(UserAuthentication company);

    /// <summary>
    /// Оборудования, которые предлагает компания.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив оборудований.</returns>
    Task<Equipment[]> GetEquipments(UserAuthentication company);

    /// <summary>
    /// Получение выполненных заказов компанией.
    /// </summary>
    /// <param name="user">Пользователь для авторизици.</param>
    /// <returns>Количество выполненных заказов.</returns>
    Task<int> GetOrdersTaken(UserAuthentication user);

    /// <summary>
    /// Получние заказов.
    /// </summary>
    /// <param name="user">Пользователь для атворизации.</param>
    /// <returns>Массив заказов (у данного пользователя).</returns>
    Task<Order[]> GetOrders(UserAuthentication user);

    /// <summary>
    /// Получение компаний, которые имеют в аренду <paramref name="equipmentId"/>.
    /// </summary>
    /// <param name="equipmentId">Id оборудования.</param>
    /// <returns>Массив компаний.</returns>
    Task<Company[]> GetCompanyWithEquipment(int equipmentId);

    /// <summary>
    /// Добавление компании.
    /// </summary>
    /// <param name="company">Добавляемая комания.</param>
    /// <returns>Добавленная компания.</returns>
    Task<Company> AddCompany(Company company);

    /// <summary>
    /// Обновелние компании.
    /// </summary>
    /// <param name="company">Обновленная комания.</param>
    /// <returns>Обновленная комания.</returns>
    Task<Company> UpdateInfoCompany(Company company);

    /// <summary>
    /// Измененение рейтинга.
    /// </summary>
    /// <param name="company">Комания с изменным рейтингом.</param>
    /// <returns>Комания с изменным рейтингом.</returns>
    Task<Company> UpdateRating(Company company);

    /// <summary>
    /// Изменение подписки.
    /// </summary>
    /// <param name="company">Компания с измененной подпиской.</param>
    /// <returns>Компания с измененной подпиской.</returns>
    Task<Company> UpdateSubscription(Company company);

    /// <summary>
    /// Взятие заказа.
    /// </summary>
    /// <param name="company">Компания, которая берет заказ.</param>
    /// <param name="orderId">Id заказа.</param>
    Task TakeOrder(UserAuthentication company, int orderId);

    /// <summary>
    /// Удаление (отказ) от заказа.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    Task RemoveOrder(int orderId);

    Task<bool> PushMailToCustomer(Feedback feedback);

    /// <summary>
    /// Взятие работы.
    /// </summary>
    /// <param name="work">Работа.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    /// <returns></returns>
    Task TakeWork(Work work, Company company, Handcraft handcraft);

    /// <summary>
    /// Взятие оборудования.
    /// </summary>
    /// <param name="equipment">Оборудование.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    /// <returns></returns>
    Task TakeEquipment(Equipment equipment, Company company, Handcraft handcraft);
}