using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mime;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;
using Microsoft.AspNetCore.Http;

namespace DataAccess;

public class DataSqlUser
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    // Добавление
    public async Task<User> Add(User user)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "INSERT INTO dbo.Users ([CountMadeOrder], [LastName], [Name], [Patronymic]," +
            "[DateOfBrith], [Phone], [CityId], [Photo], [Email], [Password], [LinkTelegram], [LinkVk])" +
            "VALUES" +
            $"(\'{user.LastName}\', \'{user.Name}\', \'{user.Patronymic}\', \'{user.DateOfBrith}\'," +
            $"\'{user.Phone}\', {user.CityId}, {user.Image}, \'{user.Email}\', \'{user.Password}\'," +
            $"\'{user.LinkTelegram}\', \'{user.LinkVk}\' GO", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(new UserAuthentication { Login = user.Email, Password = user.Password });
    }

    public async Task<Order> AddOrder(Order order)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var query = "insert into Orders (miniDescription, description, price, userId, cityId, date, example)" +
                    $" values (@miniDescription, @description, @price, @userId, (select CityId from Cities c where c.CityName = \'{order.NameCity}\')" +
                    ", @date, @example)";
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@miniDescription", order.MiniDescription);
        command.Parameters.AddWithValue("@description", order.Description);
        command.Parameters.AddWithValue("@price", order.Price);
        command.Parameters.AddWithValue("@userId", order.UserId);
        command.Parameters.AddWithValue("@date", order.Date);
        command.Parameters.AddWithValue("@example", order.Example);

        await using var reader = await command.ExecuteReaderAsync();
        await connection.CloseAsync();
        await connection.OpenAsync();
        var commandCount = new SqlCommand(
            $"UPDATE Users SET CountMadeOrder = CountMadeOrder + 1 WHERE UserId = {order.UserId}" +
            " SELECT MAX(OrderId) FROM Orders;"
            , connection);
        await using var reader1 = await commandCount.ExecuteReaderAsync();
        var orderId = 0;
        if (reader1.HasRows && reader1.ReadAsync().Result)
            orderId = (int)reader1.GetValue(0);

        await connection.CloseAsync();
        return await GetOrder(orderId);
    }

    public async Task AddFeedbackToEmployer(Feedback feedback)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            $"insert into Feedback values ({feedback.UserId}, (select CompanyId from Orders o where o.OrderId = {feedback.OrderId})," +
            $"(select HandcraftId from Orders o where o.OrderId = {feedback.OrderId}), {feedback.OrderId}, \'{feedback.Description}\')",
            connection);

        await using var reader = await command.ExecuteReaderAsync();
    }

    public async Task<User> Update(User user)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        // TODO возможно разбить
        var command = new SqlCommand(
            "UPDATE dbo.Users SET" +
            "[CountMadeOrders] = " + user.CountMadeOrders +
            "[LastName] = " + user.LastName +
            ",[Name] = " + user.Name +
            ",[Patronymic] = " + user.Patronymic +
            ",[DateOfBrith] = " + user.DateOfBrith +
            ",[Phone] = " + user.Phone +
            ",[CityId] = " + user.CityId +
            ",[Photo] = " + user.Image +
            ",[Email] = " + user.Email +
            ",[Password] = " + user.Password +
            ",[LinkTelegram] = " + user.LinkTelegram +
            ",[LinkVk] = " + user.LinkVk +
            "WHERE Email = " + user.Email +
            "GO", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await Get(new UserAuthentication { Login = user.Email, Password = user.Password });
    }

    public async Task<Order> UpdateOrder(Order order)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "UPDATE Orders SET" +
            $"Description = '{order.Description}'," +
            $"MiniDescription = {order.MiniDescription}," +
            $"Price = {order.Price}" +
            $"WHERE OrderId = {order.Id}", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetOrder(order.Id);
    }

    // Получение 
    public async Task<User> Get(UserAuthentication user)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command =
            new SqlCommand(
                $"select * from dbo.Users where email = \'{user.Login}\' " +
                $"and password = \'{user.Password}\'",
                connection);
        var customerGet = new User();
        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            customerGet.CountMadeOrders = (int)reader.GetValue(1);
            customerGet.LastName = reader.GetValue(2).ToString();
            customerGet.Name = reader.GetValue(3).ToString();
            customerGet.Patronymic = reader.GetValue(4).ToString();
            customerGet.DateOfBrith = reader.GetValue(5).ToString();
            customerGet.Phone = reader.GetValue(6).ToString();
            customerGet.CityId = (int)reader.GetValue(7);
            var obj = reader.GetValue(8);
            customerGet.Image = obj == typeof(DBNull) ? Array.Empty<byte>() : (byte[])obj;
            customerGet.Email = reader.GetValue(9).ToString();
            customerGet.Password = reader.GetValue(10).ToString();
            customerGet.LinkTelegram = reader.GetValue(11).ToString();
            customerGet.LinkVk = reader.GetValue(12).ToString();
        }

        return customerGet;
    }

    public async Task<User[]> GetUsers()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand("select * from Users", connection);
        var users = new List<User>();

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var id = (int)reader.GetValue(0);
                var countMade = (int)reader.GetValue(1);
                var lastname = reader.GetValue(2).ToString();
                var name = reader.GetValue(3).ToString();
                var patronymic = reader.GetValue(4).ToString();
                var dateOfBr = reader.GetValue(5).ToString(); // reader.GetValue(5);
                var phone = reader.GetValue(6).ToString();
                var city = (int)reader.GetValue(7);
                var photo = (byte[])reader.GetValue(8);
                var email = reader.GetValue(9).ToString();
                var password = reader.GetValue(10).ToString();
                var linkT = reader.GetValue(11).ToString();
                var linkVk = reader.GetValue(12).ToString();
                users.Add(new User
                {
                    Id = id, CountMadeOrders = countMade, Name = name, LastName = lastname, Patronymic = patronymic,
                    DateOfBrith = dateOfBr, Phone = phone, CityId = city, Image = photo, Email = email,
                    Password = password,
                    LinkTelegram = linkT, LinkVk = linkVk
                });
            }
        }

        return users.ToArray();
    }

    public async Task<User[]> GetUsersFromCity(string nameCity)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "select CountMadeOrder, LastName, Name, Patronymic, DateOfBrith, Phone, Photo, Email, Password, " +
            "LinkTelegram, LinkVk, CityName from Users INNER JOIN " +
            "Cities ON dbo.Users.CityId = dbo.Cities.CityId " +
            $"where dbo.Cities.CityName = \'{nameCity}\'", connection);
        var users = new List<User>();

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var countOrder = (int)reader.GetValue(1);
                var lastName = reader.GetValue(2).ToString();
                var name = reader.GetValue(3).ToString();
                var patronymic = reader.GetValue(4).ToString();
                var dateOfBrith = reader.GetValue(5).ToString();
                var phone = reader.GetValue(6).ToString();
                var image = (byte[])reader.GetValue(7);
                var email = reader.GetValue(8).ToString();
                var password = reader.GetValue(9).ToString();
                var linkTelegram = reader.GetValue(10).ToString();
                var linkVk = reader.GetValue(11).ToString();
                users.Add(new User
                {
                    Name = name, LastName = lastName, Patronymic = patronymic, CountMadeOrders = countOrder,
                    DateOfBrith = dateOfBrith, Phone = phone, Image = image, Email = email,
                    Password = password, LinkTelegram = linkTelegram, LinkVk = linkVk
                });
            }
        }

        return users.ToArray();
    }

    public async Task<Order> GetOrder(int orderId)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand($"select * from Orders WHERE OrderId = {orderId}", connection);
        await using var reader = await command.ExecuteReaderAsync();
        var order = new Order();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            order.Id = (int)reader.GetValue(0);
            order.MiniDescription = reader.GetValue(1).ToString();
            order.Description = reader.GetValue(2).ToString();
            order.GetOrder = (bool)reader.GetValue(3);
            order.CompletedOrder = (bool)reader.GetValue(4);
            order.Price = (int)reader.GetValue(5);
            order.UserId = (int)reader.GetValue(6);
            order.Date = reader.GetValue(9).ToString();
            order.Example = (byte[])reader["Example"]; // проверить
        }

        await connection.CloseAsync();

        // await connection.OpenAsync();
        // var cmd = new SqlCommand($"select (Example) from Orders where OrderId = {orderId}", connection);
        // var rear = await cmd.ExecuteReaderAsync();
        // await rear.ReadAsync();
        // order.Example = (byte[])rear["Example"];
        // // MemoryStream photo = new MemoryStream();
        // // order.Example = Image.FromStream(photo);
        // await connection.CloseAsync();

        return order;
    }

    public async Task<Order[]> GetOrders()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "select OrderId, MiniDescription, o.Description, CityName, Price, Date, NameCategory, NameJob, Example" +
            " from Orders o " +
            "left join Cities C on o.CityId = C.CityId " +
            "left join CategoriesJobs CJ on o.TypeJobId = CJ.CategoryJobId " +
            "left join Jobs J on CJ.CategoryJobId = J.CategoryJobId " +
            "where o.GetOrder = 0 " +
            "and o.CompletedOrder = 0", connection);
        var orders = new List<Order>();

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var id = (int)reader.GetValue(0);
                var miniDescription = reader.GetValue(1).ToString();
                var description = reader.GetValue(2).ToString();
                var city = reader.GetValue(3).ToString();
                var price = (int)reader.GetValue(4);
                var date = reader.GetValue(5)?.ToString();
                var nameCategory = reader.GetValue(6)?.ToString();
                var nameJob = reader.GetValue(6)?.ToString();
                var photo = (byte[])reader["Example"];
                orders.Add(new Order
                {
                    Id = id, MiniDescription = miniDescription, Description = description, NameCity = city,
                    Price = price, Date = date, CategoryWork = nameCategory, Work = nameJob, Example = photo
                });
            }
        }

        return orders.ToArray();
    }

    /// Получение заказов у конкретного пользователя
    public async Task<Order[]> ReceivingOrders(UserAuthentication userAuth)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var user = await Get(userAuth);

        var command = new SqlCommand(
            "SELECT Description, ServiceId, GetOrder, CompletedOrder, Price " +
            $" FROM dbo.Orders INNER JOIN dbo.Users ON \'{user.Id}\' = UserId", connection);
        var orders = new List<Order>();

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var description = reader.GetValue(1).ToString();
                var miniDescription = reader.GetValue(2).ToString();
                var getOrder = (bool)reader.GetValue(3);
                var completedOrder = (bool)reader.GetValue(4);
                var price = (int)reader.GetValue(5);
                orders.Add(new Order
                {
                    Description = description, MiniDescription = miniDescription, GetOrder = getOrder,
                    CompletedOrder = completedOrder, Price = price
                });
            }
        }

        return orders.ToArray();
    }
}