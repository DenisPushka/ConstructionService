using Domain.Models.Users;

namespace Api.Models;

public class UserCompanyHandcraft
{
    public User User { get; set; }
    public Company Company { get; set; }
    public Handcraft Handcraft { get; set; }
}