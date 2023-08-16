using System.Data.SqlClient;
using System.Diagnostics;
using DataAccess.models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess;

/// <summary>
/// Реализатор запроса для сервиса.
/// </summary>
/// КР (категорий работ),
/// ТО (тип оборудования).
public class DataSqlService
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    #region Add

    /// <summary>
    /// Добавление сервиса.
    /// </summary>
    /// <param name="service">Добавляемый сервис.</param>
    /// <returns>Массив сервисов.</returns>
    public async Task<Service[]> AddService(Service service)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"INSERT INTO Services VALUES (\'{service.Name}\', \'{service.Description}\')", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetServices();
    }

    /// <summary>
    /// Добавление КР.
    /// </summary>
    /// <param name="categoryWork">КР.</param>
    /// <returns>Массив КР.</returns>
    public async Task<CategoryWork[]> AddCategoryWork(CategoryWork categoryWork)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"INSERT INTO CategoriesJobs VALUES (\'{categoryWork.Name}\', {categoryWork.ServiceId})", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetCategoriesWork();
    }

    /// <summary>
    /// Добавление (создание) работы.
    /// </summary>
    /// <param name="work">Работа.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    /// <returns>Работа.</returns>
    public async Task<Work> AddWork(Work work, Company company, Handcraft handcraft)
    {
        await using var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync();
            var command = new SqlCommand($"INSERT INTO Jobs VALUES (\'{work.Name}\'," +
                                         $" \'{work.Description}\', \'{work.CategoryWorkId}\')", connection);
            await using var reader = await command.ExecuteReaderAsync();
            await connection.CloseAsync();

            await connection.OpenAsync();
            // создание связи многие ко многим
            command = new SqlCommand(
                $"INSERT INTO JobsSubject VALUES ((select CompanyId from Company where Email = \'{company.Email}\'), " +
                $"(select HandCraftId from Handcrafts where Email = \'{handcraft.Email}\'), " +
                "(select Max(JobId) from Jobs))", connection);
            await using var reader1 = await command.ExecuteReaderAsync();
            await connection.CloseAsync();
            return await GetWork(work.Name);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return new Work();
        }
    }

    /// <summary>
    /// Добавление ТО.
    /// </summary>
    /// <param name="equipment">ТО.</param>
    /// <returns>Массив ТО.</returns>
    public async Task<TypeEquipment[]> AddTypeEquipment(TypeEquipment type)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"INSERT INTO TypesEquipments VALUES ({type.ServiceId}, \'{type.Name}\')", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetTypeEquipments();
    }

    /// <summary>
    /// Добавление оборудования.
    /// </summary>
    /// <param name="equipment">Оборудование.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    /// <returns></returns>
    // todo переделать
    public async Task<Equipment> AddEquipment(Equipment equipment, Company company, Handcraft handcraft)
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

        await connection.OpenAsync();
        // создание связи многие ко многим
        command = new SqlCommand(
            $"INSERT INTO EquipmentSubject VALUES ((select CompanyId from Company where Email = \'{company.Email}\'), +" +
            // $" (select HandCraftId from Handcrafts), (select Max(JobId) from Jobs))", connection);
            " 1, (select Max(JobId) from Jobs))", connection);
        await using var reader1 = await command.ExecuteReaderAsync();
        await connection.CloseAsync();

        return await GetEquipment(id);
    }

    #endregion

    #region Update

    /// <summary>
    /// Обновление работы.
    /// </summary>
    /// <param name="work">Обновленная работа.</param>
    /// <returns>Обновленная работа.</returns>
    public async Task<Work> UpdateInfoAboutWork(Work work)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "UPDATE Jobs SET " +
            $"NameJob = \'{work.Name}\'," +
            $"Description = \'{work.Description}\'," +
            $"CategoryId = {work.CategoryWorkId}" +
            $"WHERE NameJob = \'{work.Name}\'", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetWork(work.Name);
    }

    /// <summary>
    /// Обновление оборудования.
    /// </summary>
    /// <param name="equipment">Обновленное оборудование.</param>
    /// <returns>Обновленное оборудование.</returns>
    public async Task<Equipment> UpdateInfoAboutEquipment(Equipment equipment)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "UPDATE Equipment SET " +
            $"Name = \'{equipment.Name}\'," + // todo удоставериться, что запрос пройдет
            $"Description = \'{equipment.Description}\'," +
            $"TypeEquipmentId = {equipment.TypeEquipmentId}" +
            $"WHERE EquipmentId = {equipment.Id}", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetEquipment(equipment.Id);
    }

    #endregion

    #region Get

    /// <summary>
    /// Получение работы.
    /// </summary>
    /// <param name="nameWork">Название работы.</param>
    /// <returns>Работа.</returns>
    public async Task<Work> GetWork(string? name)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand($"select * from Jobs where NameJob = \'{name}\' ", connection);
        var getWork = new Work();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            getWork.Id = (int)reader.GetValue(0);
            getWork.Name = reader.GetValue(1).ToString();
            getWork.Description = reader.GetValue(2).ToString();
            getWork.CategoryWorkId = (int)reader.GetValue(3);
        }

        return getWork;
    }

    /// <summary>
    /// Работы, которые предлагает компания.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив работ.</returns>/// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <param name="equipmentId">Id оборудования.</param>
    /// <returns>Оборудование.</returns>
    public async Task<Work[]> GetWorksCompany(UserAuthentication company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select J.NameJob, J.Description, J.JobId " +
                                     "from Company " +
                                     "left join JobsSubject JS on Company.CompanyId = JS.CompanyId " +
                                     "left join Jobs J on JS.JobId = J.JobId " +
                                     $"where Email = \'{company.Login}\' and Password = \'{company.Password}\'",
            connection);
        var works = new List<Work>();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var work = new Work
                {
                    Name = reader.GetValue(0).ToString(),
                    Description = reader.GetValue(1).ToString(),
                    Id = (int)reader.GetValue(2)
                };
                works.Add(work);
            }
        }

        return works.ToArray();
    }

    /// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <param name="equipmentId">Id оборудования.</param>
    /// <returns>Оборудование.</returns>
    public async Task<Equipment> GetEquipment(int equipmentId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand($"select * from Equipment where EquipmentId = {equipmentId}", connection);
        var equipment = new Equipment();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            equipment.Id = (int)reader.GetValue(0);
            equipment.Name = reader.GetValue(1).ToString();
            equipment.Description = reader.GetValue(2).ToString();
            equipment.TypeEquipmentId = (int)reader.GetValue(3);
        }

        return equipment;
    }

    /// <summary>
    /// Оборудования, которые предлагает компания.
    /// </summary>
    /// <param name="company">Компания.</param>
    /// <returns>Массив оборудований.</returns>
    public async Task<Equipment[]> GetEquipmentsCompany(UserAuthentication company)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select E.Name, E.Description, E.EquipmentId from Company " +
                                     "left join EquipmentsSubject ES on Company.CompanyId = ES.CompanyId " +
                                     "left join Equipments E on ES.EquipmentId = E.EquipmentId " +
                                     $"where Email = \'{company.Login}\' and Password = \'{company.Password}\'",
            connection);
        var equipments = new List<Equipment>();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var equipment = new Equipment
                {
                    Name = reader.GetValue(0).ToString(),
                    Description = reader.GetValue(1).ToString(),
                    Id = (int)reader.GetValue(2)
                };
                equipments.Add(equipment);
            }
        }

        return equipments.ToArray();
    }

    /// <summary>
    /// Получение сервисов.
    /// </summary>
    /// <returns>Массив сервисов.</returns>
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

    /// <summary>
    /// Получение КР.
    /// </summary>
    /// <returns>Массив КР.</returns>
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
                        Id = (int)reader.GetValue(0),
                        Name = reader.GetValue(1).ToString(),
                        ServiceId = (int)reader.GetValue(2)
                    }
                );
            }
        }

        return categoryWorks.ToArray();
    }

    /// <summary>
    /// Получение ТО.
    /// </summary>
    /// <returns>Массив ТО.</returns>
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
                        Id = (int)reader.GetValue(0),
                        ServiceId = (int)reader.GetValue(1),
                        Name = reader.GetValue(2).ToString()
                    }
                );
            }
        }

        return typeEquipments.ToArray();
    }

    /// <summary>
    /// Получение работ.
    /// </summary>
    /// <returns>Массив работ.</returns>
    public async Task<Work[]> GetWorks()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select * from Jobs", connection);
        var works = new List<Work>();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                works.Add(
                    new Work
                    {
                        Id = (int)reader.GetValue(0),
                        Name = reader.GetValue(1).ToString(),
                        Description = reader.GetValue(2).ToString(),
                        CategoryWorkId = (int)reader.GetValue(3)
                    }
                );
            }
        }

        return works.ToArray();
    }

    /// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <returns>Массив оборудований.</returns>
    public async Task<Equipment[]> GetEquipments()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select * from Equipments", connection);
        var equipments = new List<Equipment>();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                equipments.Add(
                    new Equipment
                    {
                        Id = (int)reader.GetValue(0),
                        Name = reader.GetValue(1).ToString(),
                        Description = reader.GetValue(2).ToString(),
                        TypeEquipmentId = (int)reader.GetValue(3)
                    }
                );
            }
        }

        return equipments.ToArray();
    }

    #endregion

    /// <summary>
    /// Взятие работы.
    /// </summary>
    /// <param name="work">Работа.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    public async Task TakeWork(Work work, Company company, Handcraft handcraft)
    {
        await using var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync();
            // создание связи многие ко многим
            var command = new SqlCommand(
                $"INSERT INTO JobsSubject VALUES ((select CompanyId from Company where Email = \'{company.Email}\'), +" +
                // todo при имзменение таблицы ремесленников, изменить тут строку 
                // $" (select HandCraftId from Handcrafts), (select Max(JobId) from Jobs))", connection);
                $" 1, (select JobId from Jobs where NameJob = \'{work.Name}\'))", connection);
            await using var reader1 = await command.ExecuteReaderAsync();
            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw new Exception();
        }
    }

    /// <summary>
    /// Взятие оборудования.
    /// </summary>
    /// <param name="equipment">Оборудование.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    public async Task TakeEquipment(Equipment equipment, Company company, Handcraft handcraft)
    {
        await using var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync();
            // создание связи многие ко многим
            var command = new SqlCommand(
                $"INSERT INTO EquipmentsSubject VALUES ((select CompanyId from Company where Email = \'{company.Email}\'), +" +
                // todo при имзменение таблицы ремесленников, изменить тут строку 
                // $" (select HandCraftId from Handcrafts), (select Max(JobId) from Jobs))", connection);
                $" 1, (select EquipmentId from Equipments where Name = \'{equipment.Name}\'))", connection);
            await using var reader1 = await command.ExecuteReaderAsync();
            await connection.CloseAsync();
        }
        catch (Exception e)
        {
            throw new Exception();
        }
    }
}