using DataAccess.models;
using Domain.Models.Service;

namespace Api.Models;

public class AddEquipmentCompany
{
    public UserAuthentication UserAuthentication { get; set; }
    public Equipment Equipment { get; set; }
}