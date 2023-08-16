using System.Data.SqlClient;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;
using SqlCommand = System.Data.SqlClient.SqlCommand;

namespace DataAccess;

/// <summary>
/// Реализатор запросов для компании.
/// </summary>
/// КР (категорий работ),
/// ТО (тип оборудования).
public class DataSqlCompany
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    #region Get

    /// <summary>
    /// Получение компании.
    /// </summary>
    /// <param name="company">Пользователь для авторизации.</param>
    /// <returns>Компания.</returns>
    public async Task<Company> Get(UserAuthentication company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                "SELECT Name, Company.Description, Email, Password, RatingId, S.Description " +
                "FROM Company left outer join Subscriptions S on Company.SubscriptionId = S.SubscriptionId" +
                $" WHERE Email = \'{company.Login}\' and Password = \'{company.Password}\'",
                connection);

        await using var reader = await command.ExecuteReaderAsync();
        var companyGet = new Company();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            companyGet.Name = reader.GetValue(0).ToString();
            companyGet.Description = reader.GetValue(1).ToString();
            companyGet.Email = reader.GetValue(2).ToString();
            companyGet.Password = reader.GetValue(3).ToString();
            companyGet.Rating = (int)reader.GetValue(4);
            companyGet.Subscription = reader.GetValue(5).ToString();
        }

        return companyGet;
    }

    /// <summary>
    /// Получение всех компаний.
    /// </summary>
    /// <returns>Массив компаний.</returns>
    public async Task<Company[]> GetAllCompany()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                "SELECT Name, Company.Description, Email, Password, RatingId, S.Description, Phone, Link, CityName, Street, Home " +
                "FROM Company " +
                "left outer join Subscriptions S on Company.SubscriptionId = S.SubscriptionId " +
                "left join ContactCompany CC on Company.CompanyId = CC.CompanyId " +
                "left join Addresses A on CC.ContactCompanyId = A.ContactCompanyId " +
                "left join Cities C on A.CityId = C.CityId",
                connection);
        var allCompany = new List<Company>();
        try
        {
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.ReadAsync().Result)
                {
                    allCompany.Add(new Company
                    {
                        Name = reader.GetValue(0).ToString(),
                        Description = reader.GetValue(1).ToString(),
                        Email = reader.GetValue(2).ToString(),
                        Password = reader.GetValue(3).ToString(),
                        Rating = (int)reader.GetValue(4),
                        Subscription = reader.GetValue(5).ToString(),
                        Phone = reader.GetValue(6).ToString(),
                        Link = reader.GetValue(7).ToString(),
                        CityName = reader.GetValue(8).ToString(),
                        Street = reader.GetValue(9).ToString(),
                        Home = reader.GetValue(10).ToString()
                    });
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return allCompany.ToArray();
        }

        await connection.CloseAsync();

        return allCompany.ToArray();
    }

    /// <summary>
    /// Вся информация о <paramref name="company"/>.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Компания со всеми данными о ней.</returns>
    public async Task<Company> GetAllInfoAboutCompany(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "SELECT name, Company.description, email, password, ratingId, phone, link, CityName, Street, Home, S.Description" +
            " FROM Company INNER JOIN ContactCompany ON Company.CompanyId = ContactCompany.CompanyId " +
            " left join Addresses ON ContactCompany.ContactCompanyId = Addresses.ContactCompanyId " +
            " left join Cities ON Addresses.CityName = Cities.CityName" +
            " left join Subscriptions S on Company.SubscriptionId = S.SubscriptionId" +
            $" WHERE Email = \'{company.Email}\' AND Password = \'{company.Password}\'", connection);

        await using var reader = await command.ExecuteReaderAsync();
        var companyGet = new Company();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            companyGet.Name = reader.GetValue(0).ToString();
            companyGet.Description = reader.GetValue(1).ToString();
            companyGet.Email = reader.GetValue(2).ToString();
            companyGet.Password = reader.GetValue(3).ToString();
            companyGet.Rating = (int)reader.GetValue(4);
            companyGet.Phone = reader.GetValue(5).ToString();
            companyGet.Link = reader.GetValue(6).ToString();
            companyGet.CityName = reader.GetValue(7).ToString();
            companyGet.Street = reader.GetValue(8).ToString();
            companyGet.Home = reader.GetValue(9).ToString();
            companyGet.Subscription = reader.GetValue(10).ToString();
        }

        return companyGet;
    }

    /// <summary>
    /// Получние компаний из <paramref name="city"/>.
    /// </summary>
    /// <param name="city">Город, по которому идет поиск.</param>
    /// <returns>Массив компаний.</returns>
    public async Task<Company[]> GetCompanyFromCity(string city)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                "SELECT Name, Company.Description, Email, Password, RatingId, S.Description, Phone, Link, CityName, Street, Home " +
                "FROM Company " +
                "left outer join Subscriptions S on Company.SubscriptionId = S.SubscriptionId " +
                "left join ContactCompany CC on Company.CompanyId = CC.CompanyId " +
                "left join Addresses A on CC.ContactCompanyId = A.ContactCompanyId " +
                "left join Cities C on A.CityName = C.CityName " +
                $"where (select CityName from Cities City where A.CityName = City.CityName) = \'{city}\'",
                connection);
        var allCompany = new List<Company>();
        try
        {
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.ReadAsync().Result)
                {
                    allCompany.Add(new Company
                    {
                        Name = reader.GetValue(0).ToString(),
                        Description = reader.GetValue(1).ToString(),
                        Email = reader.GetValue(2).ToString(),
                        Password = reader.GetValue(3).ToString(),
                        Rating = (int)reader.GetValue(4),
                        Subscription = reader.GetValue(5).ToString(),
                        Phone = reader.GetValue(6).ToString(),
                        Link = reader.GetValue(7).ToString(),
                        CityName = reader.GetValue(8).ToString(),
                        Street = reader.GetValue(9).ToString(),
                        Home = reader.GetValue(10).ToString()
                    });
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return allCompany.ToArray();
        }

        return allCompany.ToArray();
    }

    // просмотреть
    /// <summary>
    /// Получение отзывов о компании.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив отзывов.</returns>
    public async Task<Feedback[]> GetFeedbacks(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "select Userid, Description From Feedbacks" +
            $" WHERE CompanyId = (select CompanyId from Company c Where c.Email = \'{company.Email}\')"
            , connection);

        await using var reader = await command.ExecuteReaderAsync();
        var feedbacks = new List<Feedback>();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var userId = (int)reader.GetValue(0);
                var description = reader.GetValue(1).ToString();
                feedbacks.Add(new Feedback { UserId = userId, Description = description });
            }
        }

        await connection.CloseAsync();
        return feedbacks.ToArray();
    }

    /// <summary>
    /// Получение выполненных заказов компанией.
    /// </summary>
    /// <param name="user">Пользователь для авторизици.</param>
    /// <returns>Количество выполненных заказов.</returns>
    public async Task<int> GetOrdersTaken(UserAuthentication user)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"select NumberOfOrders - (select Count(*) from Orders where CompanyId = (select C.CompanyId from Company C where Email = \'{user.Login}\')) " +
            $"from Subscriptions where SubscriptionId = (select SubscriptionId from Company where Email = \'{user.Login}\')",
            connection);

        await using var reader = await command.ExecuteReaderAsync();
        var countOrder = 0;
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            countOrder = (int)reader.GetValue(0);
        }

        await connection.CloseAsync();
        return countOrder < 0 ? 0 : countOrder;
    }

    /// <summary>
    /// Получние заказов.
    /// </summary>
    /// <param name="company">Пользователь для атворизации.</param>
    /// <returns>Массив заказов (у данного пользователя).</returns>
    public async Task<Order[]> GetOrders(UserAuthentication company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync();
            var command =
                new SqlCommand(
                    "select OrderId, MiniDescription, o.Description, Price, UserId, CityName, " +
                    "o.TimeId, DateStart, DateEnd, CountDay, NameCategory, NameJob, Example " +
                    "from Orders o " +
                    "left join Cities C on o.CityId = C.CityId " +
                    "left join Times T on o.TimeId = T.TimeId " +
                    "left join CategoriesJobs CJ on o.TypeJobId = CJ.CategoryJobId " +
                    "left join Jobs J on o.JobId = J.JobId " +
                    $"where CompanyId = (select CompanyId from Company where Email = \'{company.Login}\')",
                    connection);

            await using var reader = await command.ExecuteReaderAsync();
            var orders = new List<Order>();
            if (reader.HasRows)
            {
                while (reader.ReadAsync().Result)
                {
                    var order = new Order
                    {
                        Id = (int)reader.GetValue(0),
                        MiniDescription = reader.GetValue(1).ToString(),
                        Description = reader.GetValue(2).ToString(),
                        Price = (int)reader.GetValue(3),
                        UserId = (int)reader.GetValue(4),
                        NameCity = reader.GetValue(5).ToString()
                    };
                    var time = new Time
                    {
                        Id = (int)reader.GetValue(6),
                        DateStart = reader.GetValue(7).ToString(),
                        DateEnd = reader.GetValue(8).ToString(),
                        CountDay = (int)reader.GetValue(9),
                    };
                    order.Time = time;
                    order.CategoryJob = reader.GetValue(10).ToString();
                    order.Job = reader.GetValue(11).ToString();
                    order.Photo = (byte[])reader["Example"];
                    orders.Add(order);
                }
            }

            return orders.ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Order[0];
        }
    }

    /// <summary>
    /// Получение компаний, которые имеют в аренду <paramref name="equipmentId"/>.
    /// </summary>
    /// <param name="equipmentId">Id оборудования.</param>
    /// <returns>Массив компаний.</returns>
    public async Task<Company[]> GetCompanyWithEquipment(int equipmentId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                "SELECT Name, Company.Description, Email, Phone, Link, CityName, Street, Home " +
                "FROM Company " +
                "left join ContactCompany CC on Company.CompanyId = CC.CompanyId " +
                "left join Addresses A on CC.ContactCompanyId = A.ContactCompanyId " +
                "left join Cities C on A.CityId = C.CityId " +
                $"where CC.CompanyId = (select CompanyId from EquipmentsSubject where EquipmentId = {equipmentId})",
                connection);
        var allCompany = new List<Company>();
        try
        {
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.ReadAsync().Result)
                {
                    allCompany.Add(new Company
                    {
                        Name = reader.GetValue(0).ToString(),
                        Description = reader.GetValue(1).ToString(),
                        Email = reader.GetValue(2).ToString(),
                        Phone = reader.GetValue(3).ToString(),
                        Link = reader.GetValue(4).ToString(),
                        CityName = reader.GetValue(5).ToString(),
                        Street = reader.GetValue(6).ToString(),
                        Home = reader.GetValue(7).ToString()
                    });
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return allCompany.ToArray();
        }

        await connection.CloseAsync();

        return allCompany.ToArray();
    }

    #endregion

    /// <summary>
    /// Добавление компании.
    /// </summary>
    /// <param name="company">Добавляемая комания.</param>
    /// <returns>Добавленная компания.</returns>
    public async Task<Company> Add(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync();
            var commandAddTableCompany =
                new SqlCommand($"INSERT INTO Company VALUES (\'{company.Name}\', \'{company.Description}\'," +
                               $" \'{company.Email}\', \'{company.Password}\', {company.Rating}, " +
                               $"(select (SubscriptionId) from Subscriptions S where S.Description = \'{company.Subscription}\'), 1); " +
                               "GO " +
                               $"INSERT INTO ContactCompany VALUES ('{company.Phone}', '{company.Link}', " +
                               "(select max(CompanyId) from Company)) " +
                               "GO " +
                               $"INSERT INTO Addresses VALUES ((select CityName FROM Cities WHERE CityName = \'{company.CityName}\'), " +
                               $"'{company.Street}', '{company.Home}', (select max(ContactCompanyId) from ContactCompany)); " +
                               "GO " +
                               "select max(CompanyId) from Company; ", connection);
            await using var reader = await commandAddTableCompany.ExecuteReaderAsync();
            if (reader.HasRows && reader.ReadAsync().Result)
                company.Id = (int)reader.GetValue(0);
            await connection.CloseAsync();
            return await GetAllInfoAboutCompany(company);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Company();
        }
    }

    #region Update

    /// <summary>
    /// Обновелние компании.
    /// </summary>
    /// <param name="company">Обновленная комания.</param>
    /// <returns>Обновленная комания.</returns>
    public async Task<Company> UpdateInfoCompany(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        company.Link ??= "";
        company.Street ??= "";
        company.Home ??= "";
        company.Phone ??= "";

        var command = new SqlCommand(
            "UPDATE Company SET " +
            $"Name = \'{company.Name}\', " +
            $"Description = \'{company.Description}\' " +
            $"where Email = \'{company.Email}\' " +
            "UPDATE ContactCompany " +
            $"set Phone = \'{company.Phone}\', " +
            $"Link  = \'{company.Link}\' " +
            $"where CompanyId = (select CompanyId from Company where Email = \'{company.Email}\') " +
            "update Addresses " +
            $"set CityId = (select CityId from Cities where CityName = \'{company.CityName}\'), " +
            $"Street = \'{company.Street}\', " +
            $"Home   = \'{company.Home}\' " +
            "where ContactCompanyId = " +
            "(select ContactCompanyId " +
            "from Company " +
            "left outer join ContactCompany CC on Company.CompanyId = CC.CompanyId " +
            $"where Email = \'{company.Email}\') ", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(new UserAuthentication { Login = company.Email, Password = company.Password });
    }

    /// <summary>
    /// Измененение рейтинга.
    /// </summary>
    /// <param name="company">Комания с изменным рейтингом.</param>
    /// <returns>Комания с изменным рейтингом.</returns>
    public async Task<Company> UpdateRating(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand("update Company SET " +
                                     $"RatingId = {company.Rating} " +
                                     $"WHERE Email = \'{company.Email}\' ", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(new UserAuthentication { Login = company.Email, Password = company.Password });
    }

    /// <summary>
    /// Изменение подписки.
    /// </summary>
    /// <param name="company">Компания с измененной подпиской.</param>
    /// <returns>Компания с измененной подпиской.</returns>
    public async Task<Company> UpdateSubscription(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand("UPDATE Company SET " +
                                     $"SubscriptionId = (select (SubscriptionId) from Subscriptions where Description = \'{company.Subscription}\') " +
                                     $"WHERE Email = \'{company.Email}\'", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(new UserAuthentication { Login = company.Email, Password = company.Password });
    }

    #endregion

    /// <summary>
    /// Взятие заказа.
    /// </summary>
    /// <param name="company">Компания, которая берет заказ.</param>
    /// <param name="orderId">Id заказа.</param>
    public async Task TakeOrder(UserAuthentication company, int orderId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand(
            "UPDATE Orders SET " +
            $"CompanyId = (SELECT CompanyId FROM Company WHERE Email = \'{company.Login}\')," +
            "GetOrder = 1" +
            $"WHERE OrderId = {orderId} " +
            "update Company set " +
            "NumberOfOrdersInProgress = NumberOfOrdersInProgress + 1 " +
            $"where Email = \'{company.Login}\'",
            connection);

        await using var reader = await command.ExecuteReaderAsync();
    }

    public async Task CompletedOrder(UserAuthentication company, int orderId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand(
            "UPDATE Orders SET " +
            "CompletedOrder = 1" +
            $"WHERE OrderId = {orderId} " +
            $"update Company set " +
            "NumberOfOrdersInProgress = NumberOfOrdersInProgress - 1 " +
            $"where Email = \'{company.Login}\'",
            connection);

        await using var reader = await command.ExecuteReaderAsync();
    }

    /// <summary>
    /// Удаление (отказ) от заказа.
    /// </summary>
    /// <param name="orderId">Id заказа.</param>
    public async Task RemoveOrder(int orderId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync();
            var command =
                new SqlCommand($"select OrderId, CompanyId, HandcraftId from Orders where OrderId = {orderId}",
                    connection);
            await using var reader1 = await command.ExecuteReaderAsync();
            var order = new Order();
            if (reader1.HasRows && reader1.ReadAsync().Result)
            {
                order.Id = (int)reader1.GetValue(0);
                order.CompanyId = (int)reader1.GetValue(1);
                order.HandcraftId = (int)reader1.GetValue(2);
            }

            await connection.CloseAsync();
            if (order.Id is 0 or 1)
                return;

            await connection.OpenAsync();
            command = new SqlCommand(
                $"INSERT INTO NotExecuteOrders VALUES ({order.Id}, {order.CompanyId}, {order.HandcraftId}); " +
                $"UPDATE Orders SET CompanyId = 1, HandcraftId = 1, GetOrder = 0 WHERE OrderId = {order.Id} ",
                connection);
            await using var reader = await command.ExecuteReaderAsync();
            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}