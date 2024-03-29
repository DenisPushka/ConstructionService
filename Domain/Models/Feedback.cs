﻿namespace Domain.Models;

/// <summary>
/// Отзыв.
/// </summary>
public class Feedback
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int HandcraftId { get; set; }
    public int OrderId { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; } = "";
}