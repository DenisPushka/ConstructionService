using System.Data.SqlClient;
using Domain.Models;
using Domain.Models.Users;
using SqlCommand = System.Data.SqlClient.SqlCommand;

namespace DataAccess;

public class DataSqlCompany
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    // TODO прописать для получения id для добавления работы или сервиса
    public async Task<Company> Get(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                $"SELECT * FROM Company WHERE Email = \'{company.Email}\' and Password = \'{company.Password}\'",
                connection);

        await using var reader = await command.ExecuteReaderAsync();
        var companyGet = new Company();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            companyGet.Id = (int)reader.GetValue(0);
            companyGet.Name = reader.GetValue(1).ToString();
            companyGet.Description = reader.GetValue(2).ToString();
            companyGet.Email = reader.GetValue(3).ToString();
            companyGet.Password = reader.GetValue(4).ToString();
            companyGet.Rating = (int)reader.GetValue(5);
        }

        return companyGet;
    }

    /// НЕ НАПИСАНО
    public async Task<Company[]> GetAllCompany()
    {
        return new[] { new Company() };
    }

    public async Task<Company> GetAllInfoAboutCompany(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "SELECT name, description, email, password, ratingId, phone, link, CityName, Street, Home" +
            " FROM Company INNER JOIN ContactCompany ON Company.CompanyId = ContactCompany.CompanyId " +
            " INNER JOIN Addresses ON ContactCompany.ContactCompanyId = Addresses.ContactCompanyId " +
            " INNER JOIN Cities ON Addresses.CityId = Cities.CityId" +
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
        }

        return companyGet;
    }

    /// НЕ НАПИСАНО!
    public async Task<Company[]> GetCompanyFromCity(string city)
    {
        return new[] { new Company() };
    }

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
                feedbacks.Add(new Feedback {UserId = userId, Description = description});
            }
        }

        return feedbacks.ToArray();
    }
    
    // ДОБАВЛЕНИЕ
    public async Task<Company> Add(Company company)
    {
        // Проверка пользователя в системе
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var commandCheck =
            new SqlCommand($"SELECT COUNT(*) FROM Company where Email = '{company.Email}'", connection);
        await using var reader0 = await commandCheck.ExecuteReaderAsync();
        if (reader0.HasRows && reader0.ReadAsync().Result)
        {
            var a = (int)reader0.GetValue(0);
            if (a != 0)
                return new Company();
        }

        await connection.CloseAsync();

        // Добавление в табл компания
        await connection.OpenAsync();
        var commandAddTableCompany =
            new SqlCommand($"INSERT INTO Company VALUES ('{company.Name}', '{company.Description}'," +
                           $" '{company.Email}', '{company.Password}', {company.Rating})" +
                           "select max(CompanyId) from Company", connection);
        await using var reader = await commandAddTableCompany.ExecuteReaderAsync();
        if (reader.HasRows && reader.ReadAsync().Result)
            company.Id = (int)reader.GetValue(0);
        await connection.CloseAsync();

        // Добавление в табл Контакт компании
        await connection.OpenAsync();
        var commandAddTableCompanyContact =
            new SqlCommand($"INSERT INTO ContactCompany VALUES ('{company.Phone}', '{company.Link}'," +
                           $" '{company.Id}')", connection);
        await using var reader2 = await commandAddTableCompanyContact.ExecuteReaderAsync();
        await connection.CloseAsync();
        await connection.OpenAsync();
        var commandAddTableAddresses =
            new SqlCommand(
                $"INSERT INTO Addresses VALUES ((select CityId FROM Cities WHERE CityName = \'{company.CityName}\'), " +
                $" '{company.Street}', '{company.Home}', {company.ContactCompanyId})",
                connection);
        await using var reader3 = await commandAddTableAddresses.ExecuteReaderAsync();
        return await GetAllInfoAboutCompany(company);
    }

    public async Task<Company> UpdateInfoCompany(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        // TODO возможно разбить
        var command = new SqlCommand("USE [ConstructionService] " +
                                     "GO UPDATE [dbo].[Company] SET " +
                                     $"[Name] = \'{company.Name}\'," +
                                     $"[Description] = \'{company.Description}\'," +
                                     $"[Password] = \'{company.Password}\'," +
                                     $"[RatingId] = \'{company.Rating}\'" +
                                     $"WHERE Email = {company.Email}" + // todo добавить ли парль?
                                     "GO", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(company);
    }

    public async Task<Company> UpdateRating(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand(
            "UPDATE Company SET" +
            $"[RatingId] = {company.Rating}" +
            $"WHERE Email = \'{company.Email}\'" +
            "GO", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(company);
    }

    public async Task TakeOrder(Company company, int orderId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand(
            "UPDATE Orders SET " +
            $"CompanyId = (SELECT CompanyId FROM Company WHERE Email = '{company.Email}')," +
            "GetOrder = 1" +
            $"WHERE OrderId = {orderId}",
            connection);

        await using var reader = await command.ExecuteReaderAsync();
    }

    public async Task RemoveOrder(int orderId, int companyId, int handcraftId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand(
            $"UPDATE Orders SET CompanyId = 0, GetOrder = 0 WHERE OrderId = {orderId}; " +
            $"INSERT INTO NotExecuteOrders VALUES ({orderId}, {companyId}, {handcraftId})",
            connection);
        await using var reader = await command.ExecuteReaderAsync();
        await connection.CloseAsync();
    }
}