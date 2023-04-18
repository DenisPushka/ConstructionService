using System.Data.SqlClient;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess;

public class DataSqlHandCraft
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    #region Get

    public async Task<Handcraft> Get(UserAuthentication handcraft)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                "SELECT Name, LastName, Patronymic, H.Description, Phone, Email, Password, CityName, LinkVk, LinkTelegram, " +
                "RatingId, S.Description " +
                "FROM HandCrafts H " +
                "left outer join Subscriptions S on H.SubscriptionId = S.SubscriptionId " +
                "left join ContactHandCrafts CHC on H.HandCraftId = CHC.HandCraftId " +
                "left outer join Cities C on C.CityName = CHC.CityName " +
                $"WHERE Email = \'{handcraft.Login}\' and Password = \'{handcraft.Password}\'",
                connection);

        await using var reader = await command.ExecuteReaderAsync();
        var handcraftGet = new Handcraft();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            handcraftGet.Name = reader.GetValue(0).ToString();
            handcraftGet.LastName = reader.GetValue(1).ToString();
            handcraftGet.Patronymic = reader.GetValue(2).ToString();
            handcraftGet.Description = reader.GetValue(3).ToString();
            handcraftGet.Phone = reader.GetValue(4).ToString();
            handcraftGet.Email = reader.GetValue(5).ToString();
            handcraftGet.Password = reader.GetValue(6).ToString();
            handcraftGet.CityName = reader.GetValue(7).ToString();
            handcraftGet.LinkVk = reader.GetValue(8).ToString();
            handcraftGet.LinkTelegram = reader.GetValue(9).ToString();
            handcraftGet.Rating = (int)reader.GetValue(10);
            handcraftGet.Subscription = reader.GetValue(11).ToString();
        }

        await connection.CloseAsync();
        return handcraftGet;
    }

    public async Task<Handcraft[]> GetHandcrafts()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                "SELECT Name, LastName, Patronymic, H.Description, Phone, Email, Password, CityName, LinkVk, LinkTelegram, " +
                "RatingId, S.Description " +
                "FROM HandCrafts H " +
                "left outer join Subscriptions S on H.SubscriptionId = S.SubscriptionId " +
                "left join ContactHandCrafts CHC on H.HandCraftId = CHC.HandCraftId " +
                "left outer join Cities C on C.CityName = CHC.CityName ",
                connection);
        var handCrafts = new List<Handcraft>();
        try
        {
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.ReadAsync().Result)
                {
                    handCrafts.Add(new Handcraft
                    {
                        Name = reader.GetValue(0).ToString(),
                        LastName = reader.GetValue(1).ToString(),
                        Patronymic = reader.GetValue(2).ToString(),
                        Description = reader.GetValue(3).ToString(),
                        Phone = reader.GetValue(4).ToString(),
                        Email = reader.GetValue(5).ToString(),
                        Password = reader.GetValue(6).ToString(),
                        CityName = reader.GetValue(7).ToString(),
                        LinkVk = reader.GetValue(8).ToString(),
                        LinkTelegram = reader.GetValue(9).ToString(),
                        Rating = (int)reader.GetValue(10),
                        Subscription = reader.GetValue(11).ToString()
                    });
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return handCrafts.ToArray();
        }

        return handCrafts.ToArray();
    }

    public async Task<Handcraft[]> GetHandCraftsFromCity(string city)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                "SELECT Name, LastName, Patronymic, H.Description, Phone, Email, Password, CityName, LinkVk, LinkTelegram, " +
                "RatingId, S.Description " +
                "FROM HandCrafts H " +
                "left outer join Subscriptions S on H.SubscriptionId = S.SubscriptionId " +
                "left join ContactHandCrafts CHC on H.HandCraftId = CHC.HandCraftId " +
                "left outer join Cities C on C.CityName = CHC.CityName " +
                $"where CityName = \'{city}\'",
                connection);
        var handCrafts = new List<Handcraft>();
        try
        {
            await using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.ReadAsync().Result)
                {
                    handCrafts.Add(new Handcraft
                    {
                        Name = reader.GetValue(0).ToString(),
                        LastName = reader.GetValue(1).ToString(),
                        Patronymic = reader.GetValue(2).ToString(),
                        Description = reader.GetValue(3).ToString(),
                        Phone = reader.GetValue(4).ToString(),
                        Email = reader.GetValue(5).ToString(),
                        Password = reader.GetValue(6).ToString(),
                        CityName = reader.GetValue(7).ToString(),
                        LinkVk = reader.GetValue(8).ToString(),
                        LinkTelegram = reader.GetValue(9).ToString(),
                        Rating = (int)reader.GetValue(10),
                        Subscription = reader.GetValue(11).ToString()
                    });
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return handCrafts.ToArray();
        }

        return handCrafts.ToArray();
    }

    #endregion

    public async Task<Handcraft> Add(Handcraft handcraft)
    {
        await using var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync();
            var commandAddTableCompany =
                new SqlCommand($"insert into HandCrafts values (\'{handcraft.Description}\', {handcraft.Rating}, " +
                               $" (select SubscriptionId from Subscriptions where Subscriptions.Description = \'{handcraft.Description}\'), " +
                               $"\'{handcraft.Email}\', \'{handcraft.Password}\'); " +
                               "GO " +
                               $"insert into ContactHandCrafts values (\'{handcraft.LastName}\', \'{handcraft.Name}\', " +
                               $"\'{handcraft.Patronymic}\', \'{handcraft.Phone}\', (select CityName from Cities where CityName = \'{handcraft.CityName}\'), " +
                               $"\'{handcraft.LinkVk}\', \'{handcraft.LinkTelegram}\', (select max(HandCraftId) from HandCrafts))",
                    connection);
            await using var reader = await commandAddTableCompany.ExecuteReaderAsync();
            await connection.CloseAsync();
            return await Get(new UserAuthentication { Login = handcraft.Email, Password = handcraft.Email });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Handcraft();
        }
    }

    #region Update

    public async Task<Handcraft> UpdateInfoHandCraft(Handcraft handcraft)
    {
        await using var connection = new SqlConnection(ConnectionString);
        try
        {
            await connection.OpenAsync();

            var command = new SqlCommand(
                $"update HandCrafts set Description = \'{handcraft.Description}\', Password = \'{handcraft.Password}\' where Email = \'{handcraft.Email}\'; " +
                $"update ContactHandCrafts set LastName = \'{handcraft.LastName}\', Name = \'{handcraft.Name}\', Patronymic = \'{handcraft.Patronymic}\', " +
                $"Phone = \'{handcraft.Phone}\', CityName = (select CityName from Cities where CityName = \'{handcraft.CityName}\'), " +
                $"LinkVk = \'{handcraft.LinkVk}\', LinkTelegram = \'{handcraft.LinkTelegram}\' " +
                $"where HandCraftId = (select HandCraftId from HandCrafts where Email = \'{handcraft.Email}\')",
                connection);

            await using var reader = await command.ExecuteReaderAsync();
            await connection.CloseAsync();
            return await Get(new UserAuthentication { Login = handcraft.Email, Password = handcraft.Password });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new Handcraft();
        }
    }

    
    public async Task<Handcraft> UpdateRating(Handcraft handcraft)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand("update HandCrafts SET " +
                                     $"RatingId = {handcraft.Rating} " +
                                     $"WHERE Email = \'{handcraft.Email}\' ", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(new UserAuthentication { Login = handcraft.Email, Password = handcraft.Password });
    }

    public async Task<Handcraft> UpdateSubscription(Handcraft handcraft)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand("UPDATE HandCrafts SET " +
                                     $"SubscriptionId = (select (SubscriptionId) from Subscriptions where Description = \'{handcraft.Subscription}\') " +
                                     $"WHERE Email = \'{handcraft.Email}\'", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(new UserAuthentication { Login = handcraft.Email, Password = handcraft.Password });
    }

    #endregion
    
    public async Task TakeOrder(UserAuthentication handcraft, int orderId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand(
            "UPDATE Orders SET " +
            $"HandcraftId = (SELECT HandCraftId FROM HandCrafts WHERE Email = \'{handcraft.Login}\')," +
            "GetOrder = 1" +
            $"WHERE OrderId = {orderId} " +
            "update HandCrafts set " +
            "NumberOfOrdersInProgress = NumberOfOrdersInProgress + 1 " +
            $"where Email = \'{handcraft.Login}\'",
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
            "update HandCrafts set " +
            "NumberOfOrdersInProgress = NumberOfOrdersInProgress - 1 " +
            $"where Email = \'{company.Login}\'",
            connection);
        await connection.CloseAsync();
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