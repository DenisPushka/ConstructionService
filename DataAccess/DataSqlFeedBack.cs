using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess;

public class DataSqlFeedBack
{
    private const string ConnectionString =
        "Server=DenisBaranovski;Database=ConstructionService;Trusted_Connection=True;TrustServerCertificate=Yes;";

    /// <summary>
    /// Отправка сообщения пользователю
    /// </summary>
    public async Task<bool> SentToUser(Feedback feedback)
    {
        // Поиск пользователя + нужно ли?

        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        var command = new SqlCommand(
            "INSERT INTO CorrectionsFromTheContractorOnTheOrder " +
            $"VALUES ({feedback.CompanyId}, {feedback.HandcraftId}, {feedback.OrderId}, '{feedback.Description}')",
            connection);

        await using var reader = await command.ExecuteReaderAsync();
        return true;
    }
    
    /// <summary>
    /// НЕ НАПИСАНО!
    /// </summary>
    public async Task<bool> SendToContactor(Feedback feedback)
    {
        return false;
    }
    
    
}