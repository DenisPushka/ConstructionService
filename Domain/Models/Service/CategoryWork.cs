﻿namespace Domain.Models.Service;

public class CategoryWork
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int ServiceId { get; set; }
}