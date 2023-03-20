using System.Data.SqlClient;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess;

public class DataSql
{
    private const string ConnectionString = "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    public async Task<bool> AddCity(City city)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
 
        var command = new SqlCommand( $"insert into Cities values ('{city.NameCity}')", connection);
        await command.ExecuteNonQueryAsync();

        return true;
    }

    public async Task<City[]> GetAllCity()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
 
        var command = new SqlCommand( $"select * from Cities", connection);
        var listCity = new List<City>();
        
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var a = reader.GetOrdinal("CityId");
                var id = (int) reader.GetValue(0);

                var b = reader.GetOrdinal("CityName");
                var name = reader.GetString(b);
                listCity.Add(new City {Id = id, NameCity = name});
            }
        }

        return listCity.ToArray();
    }

    public async Task<bool> AddCompany(Company company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
 
        SqlCommand command;

        // command = new SqlCommand( $"insert into Cities values ('{cities.NameCity}')", Connection);
        // await command.ExecuteNonQueryAsync();
        return true;
    }

    public async Task<char> Authentication(UserAuthentication userAuthentication)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
 
        // TODO суть команды считается запрос на проверку пользователя (под пользователем понимается: компания, мастера и заказчик)
        // если хоть у одного есть совпадение, то тру. Индексы на почту или(и) номер телефона
        var command = new SqlCommand( "select * from ContactCustomers", connection);
        // если есть в компании c
        // если пользоваетль то u
        // если 'h'
        return 'c';
    }
}