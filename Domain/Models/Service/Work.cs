﻿namespace Domain.Models.Service;

public class Work
{
    public int Id { get; set; }
    public string? Name { get; set; } = "";
    public string? Description { get; set; } = "";
    public int CategoryWorkId { get; set; }
}