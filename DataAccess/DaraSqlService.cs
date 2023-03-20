using System.Data.SqlClient;
using DataAccess.models;
using Domain.Models.Service;

namespace DataAccess;

public class DaraSqlService
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    public async Task<Service[]> AddService(Service service)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"INSERT INTO Services VALUES (\'{service.Name}\', \'{service.Description}\')", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetServices();
    }

    public async Task<CategoryWork[]> AddCategoryWork(CategoryWork categoryWork)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"INSERT INTO CategoriesJobs VALUES (\'{categoryWork.Name}\', {categoryWork.ServiceId})", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetCategoriesWork();
    }

    public async Task<Work> AddWork(Work work)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand($"INSERT INTO Jobs VALUES (\'{work.Name}\'," +
                                     $" \'{work.Description}\', \'{work.CategoryWorkId}\')", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetWork(work.Name);
    }

    public async Task<TypeEquipment[]> AddTypeEquipment(TypeEquipment type)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"INSERT INTO TypesEquipments VALUES ({type.ServiceId}, \'{type.Name}\')", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetTypeEquipments();
    }

    public async Task<Equipment> AddEquipment(Equipment equipment)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"INSERT INTO Equipments VALUES (\'{equipment.Name}\', \'{equipment.Description}\'," +
            $"{equipment.TypeEquipmentId})" +
            "SELECT MAX(EquipmentId) FROM Equipments", connection);

        await using var reader = await command.ExecuteReaderAsync();
        var id = 0;
        if (reader.HasRows)
            id = (int)reader.GetValue(0);
        return await GetEquipment(id);
    }

    // Обноление
    public async Task<Work> UpdateInfoAboutWork(Work work)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        // TODO возможно разбить
        var command = new SqlCommand(
            "UPDATE Jobs SET " +
            $"NameJob = \'{work.Name}\'," +
            $"Description = \'{work.Description}\'," +
            $"CategoryId = {work.CategoryWorkId}" +
            $"WHERE NameJob = \'{work.Name}\'", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetWork(work.Name);
    }

    public async Task<Equipment> UpdateInfoAboutEquipment(Equipment equipment)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        // TODO возможно разбить
        var command = new SqlCommand(
            "UPDATE Equipment SET " +
            $"Name = \'{equipment.Name}\'," + // todo удоставериться, что запрос пройдет
            $"Description = \'{equipment.Description}\'," +
            $"TypeEquipmentId = {equipment.TypeEquipmentId}" +
            $"WHERE EquipmentId = {equipment.Id}", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetEquipment(equipment.Id);
    }

    // Получение
    public async Task<Work> GetWork(string? name)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand($"select * from Jobs where Name = \'{name}\' ", connection);
        var getWork = new Work();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            getWork.Name = reader.GetValue(1).ToString();
            getWork.Description = reader.GetValue(2).ToString();
            getWork.CategoryWorkId = (int)reader.GetValue(3);
        }

        return getWork;
    }

    public async Task<Equipment> GetEquipment(int equipmentId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand($"select * from Equipment where EquipmentId = {equipmentId}", connection);
        var equipment = new Equipment();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            equipment.Name = reader.GetValue(1).ToString();
            equipment.Description = reader.GetValue(2).ToString();
            equipment.TypeEquipmentId = (int)reader.GetValue(3);
        }

        return equipment;
    }

    public async Task<Service[]> GetServices()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select * from Services", connection);
        var services = new List<Service>();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                services.Add(
                    new Service
                    {
                        Name = reader.GetValue(1).ToString(),
                        Description = reader.GetValue(2).ToString()
                    }
                );
            }
        }

        return services.ToArray();
    }

    public async Task<CategoryWork[]> GetCategoriesWork()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select * from CategoriesJobs", connection);
        var categoryWorks = new List<CategoryWork>();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                categoryWorks.Add(
                    new CategoryWork
                    {
                        Name = reader.GetValue(1).ToString(),
                        ServiceId = (int)reader.GetValue(2)
                    }
                );
            }
        }

        return categoryWorks.ToArray();
    }

    public async Task<TypeEquipment[]> GetTypeEquipments()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select * from TypesEquipments", connection);
        var typeEquipments = new List<TypeEquipment>();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                typeEquipments.Add(
                    new TypeEquipment
                    {
                        Name = reader.GetValue(1).ToString(),
                        ServiceId = (int)reader.GetValue(2)
                    }
                );
            }
        }
        
        return typeEquipments.ToArray();
    }
}