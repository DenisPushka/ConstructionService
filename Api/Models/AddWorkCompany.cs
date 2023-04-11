using DataAccess.models;
using Domain.Models.Service;

namespace Api.Models;

public class AddWorkCompany
{
    public UserAuthentication UserAuthentication { get; set; }
    public Work Work { get; set; }
}