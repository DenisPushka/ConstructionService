using System.Data.SqlClient;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess;

public class DataSql
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    public async Task<bool> AddCity(City city)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand($"insert into Cities values ('{city.NameCity}')", connection);
        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<City[]> GetAllCity()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select * from Cities", connection);
        var listCity = new List<City>();

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var id = (int)reader.GetValue(0);
                var name = reader.GetValue(1).ToString();
                listCity.Add(new City { Id = id, NameCity = name });
            }
        }

        return listCity.ToArray();
    }

    public async Task<bool> Authentication(UserAuthentication userAuthentication)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();


        var command = new SqlCommand($"select count(*) from Users us where us.Email = '{userAuthentication.Login}'" +
                                     $" and us.Password = '{userAuthentication.Password}'", connection);

        await using var reader = await command.ExecuteReaderAsync();

        var res = 0;
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            res = (int)reader.GetValue(0);
        }

        return res != 0;
        // если есть в компании c
        // если пользоваетль то u
        // если 'h'
    }

    public async Task<Subscription[]> GetSubscriptions()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select * from Subscriptions", connection);
        var subscriptions = new List<Subscription>();

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var id = (int)reader.GetValue(0);
                var description = reader.GetValue(1).ToString();
                var price = (int)reader.GetValue(2);
                subscriptions.Add(new Subscription { Id = id, Description = description, Price = price });
            }
        }

        return subscriptions.ToArray();
    }
}