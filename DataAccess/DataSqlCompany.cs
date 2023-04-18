using System.Data.SqlClient;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;
using SqlCommand = System.Data.SqlClient.SqlCommand;

namespace DataAccess;

public class DataSqlCompany
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    #region Get

    public async Task<Company> Get(UserAuthentication company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                "SELECT Name, Company.Description, Email, Password, RatingId, S.Description FROM Company left outer join Subscriptions S on Company.SubscriptionId = S.SubscriptionId" +
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
                "left join Cities C on A.CityName = C.CityName",
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

        return feedbacks.ToArray();
    }

    #endregion

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

    public async Task<Company> UpdateInfoCompany(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("UPDATE Company SET " +
                                     $"[Name] = \'{company.Name}\'," +
                                     $"[Description] = \'{company.Description}\'," +
                                     $"[Password] = \'{company.Password}\'," +
                                     $"[RatingId] = {company.Rating}" +
                                     $"[SubscriptionId] = {company.Subscription}" +
                                     $"WHERE Email = {company.Email}" +
                                     "GO", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(new UserAuthentication { Login = company.Email, Password = company.Password });
    }

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
                $"UPDATE Orders SET CompanyId = 0, HandcraftId = 0, GetOrder = 0 WHERE OrderId = {order.Id} ",
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