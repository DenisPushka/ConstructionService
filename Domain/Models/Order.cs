namespace Domain.Models;

/// <summary>
/// Заказ.
/// </summary>
public class Order
{
    public int Id { get; set; }

    /// <summary>
    /// Мини-описание.
    /// </summary>
    public string? MiniDescription { get; set; } = "";

    /// <summary>
    /// Полное описание
    /// </summary>
    public string? Description { get; set; } = "";

    /// <summary>
    /// Заказ взят.
    /// </summary>
    public bool GetOrder { get; set; }

    /// <summary>
    /// Заказ выполнен.
    /// </summary>
    public bool CompletedOrder { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Город.
    /// </summary>
    public string? NameCity { get; set; } = "";

    /// <summary>
    /// Время.
    /// </summary>
    public Time? Time { get; set; }

    /// <summary>
    /// Категория работы.
    /// </summary>
    public string? CategoryJob { get; set; } = "";

    /// <summary>
    /// Работа.
    /// </summary>
    public string? Job { get; set; } = "";

    /// <summary>
    /// Фото.
    /// </summary>
    public byte[] Photo { get; set; } = new byte[0];

    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int HandcraftId { get; set; }
}