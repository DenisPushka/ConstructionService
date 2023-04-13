using System.Data.SqlClient;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Users;

namespace DataAccess;

public class DataSqlUser
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    #region Add

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

        var query = "insert into Times (DateStart, DateEnd, CountDay, CountRemainingDay) " +
                    "values (@dateStart, @dateEnd, @countDay, @countRemainDay) " +
                    "insert into Orders (minidescription, description, price, userid, cityid, TimeId, typejobid, jobid, example) " +
                    $"values (@miniDescription, @description, @price, @userId, (select (CityId) from Cities where CityName = \'{order.NameCity}\'), (SELECT MAX(TimeId) FROM Times), @typeJobId, @jobId, @example);" +
                    " SELECT MAX(OrderId) FROM Orders";

        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@miniDescription", order.MiniDescription);
        command.Parameters.AddWithValue("@description", order.Description);
        command.Parameters.AddWithValue("@price", order.Price);
        command.Parameters.AddWithValue("@userId", order.UserId);
        // command.Parameters.AddWithValue("@time", order.Time);
        command.Parameters.AddWithValue("@typeJobId", int.Parse(order.CategoryJob));
        command.Parameters.AddWithValue("@jobId", int.Parse(order.Job));
        command.Parameters.AddWithValue("@example", order.Photo);
        command.Parameters.AddWithValue("@dateStart", order.Time.DateStart);
        command.Parameters.AddWithValue("@dateEnd", order.Time.DateEnd);
        command.Parameters.AddWithValue("@countDay", order.Time.CountDay);
        command.Parameters.AddWithValue("@countRemainDay", order.Time.CountRemainDay);

        await using var reader = await command.ExecuteReaderAsync();
        var orderId = 0;
        if (reader.HasRows && reader.ReadAsync().Result)
            orderId = (int)reader.GetValue(0);

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
    

    #endregion

    #region Update

    public async Task<User> Update(User user)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

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
            $"CityId = {order.NameCity}" +
            $"TimeId = {order.Time}" +
            $"TypeJobId = {order.CategoryJob}" +
            $"JobId = {order.Job}" +
            $"Example = {order.Time}" +
            $"WHERE OrderId = {order.Id}", connection);

        await using var reader = await command.ExecuteReaderAsync();
        return await GetOrder(order.Id);
    }
    

    #endregion

    #region Get

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
                var dateOfBr = reader.GetValue(5).ToString();
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

        var command = new SqlCommand(
            "select OrderId, MiniDescription, o.Description, Price, UserId, CityName, o.TimeId, DateStart, DateEnd, CountDay, NameCategory, NameJob, Example " +
            "from Orders o " +
            "left join Cities C on o.CityId = C.CityId " +
            "left join Times T on o.TimeId = T.TimeId " +
            "left join CategoriesJobs CJ on o.TypeJobId = CJ.CategoryJobId " +
            "left join Jobs J on o.JobId = J.JobId " +
            $"where o.OrderId = {orderId} ", connection);
        await using var reader = await command.ExecuteReaderAsync();
        var order = new Order();
        if (reader.HasRows && reader.ReadAsync().Result)
        {
            order.Id = (int)reader.GetValue(0);
            order.MiniDescription = reader.GetValue(1).ToString();
            order.Description = reader.GetValue(2).ToString();
            order.Price = (int)reader.GetValue(3);
            order.UserId = (int)reader.GetValue(4);
            order.NameCity = reader.GetValue(5).ToString();
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
        }

        await connection.CloseAsync();
        return order;
    }

    public async Task<Order[]> GetOrders()
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();

        var command = new SqlCommand(
            "select OrderId, MiniDescription, o.Description, Price, UserId, CityName, o.TimeId, DateStart, DateEnd, CountDay, NameCategory, NameJob, Example " +
            "from Orders o " +
            "left join Cities C on o.CityId = C.CityId " +
            "left join Times T on o.TimeId = T.TimeId " +
            "left join CategoriesJobs CJ on o.TypeJobId = CJ.CategoryJobId " +
            "left join Jobs J on o.JobId = J.JobId " +
            "where o.GetOrder = 0 " +
            "and o.CompletedOrder = 0", connection);
        var orders = new List<Order>();

        await using var reader = await command.ExecuteReaderAsync();

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

    /// Получение заказов у конкретного пользователя
    public async Task<Order[]> ReceivingOrders(UserAuthentication userAuth)
    {
        await using var connection = new SqlConnection(ConnectionString);
        var command = new SqlCommand(
            "SELECT OrderId, MiniDescription, o.Description, GetOrder, CompletedOrder, Price, UserId,CityName, NameJob" +
            " FROM  Orders o " +
            "left join Cities C on o.CityId = C.CityId " +
            "left join Times T on o.TimeId = T.TimeId " +
            "left join CategoriesJobs CJ on o.TypeJobId = CJ.CategoryJobId " +
            "left join Jobs J on o.JobId = J.JobId " +
            $"inner join Users on o.UserId = (select (UserId) from Users where Email = \'{userAuth.Login}\')",
            connection);
        var orders = new List<Order>();

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.ReadAsync().Result)
            {
                var order = new Order
                {
                    Id = (int)reader.GetValue(0),
                    MiniDescription = reader.GetValue(1).ToString(),
                    Description = reader.GetValue(2).ToString(),
                    GetOrder = (bool)reader.GetValue(3),
                    CompletedOrder = (bool)reader.GetValue(4),
                    Price = (int)reader.GetValue(5),
                    UserId = (int)reader.GetValue(6),
                    NameCity = reader.GetValue(7).ToString(),
                    Job = reader.GetValue(8).ToString()
                };
                orders.Add(order);
            }
        }

        return orders.ToArray();
    }

    #endregion
}