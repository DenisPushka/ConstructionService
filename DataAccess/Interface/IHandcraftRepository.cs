using DataAccess.models;
using Domain.Models.Users;

namespace DataAccess.Interface;

public interface IHandcraftRepository
{
    Task<Handcraft> GetHandicraft(UserAuthentication user);
    Task<Handcraft[]> GetAllHandicraft();
    Task<Handcraft[]> GetHandicraftToCity(string city);
    

    IAsyncResult AddHandicraft(Handcraft handcraft);
    Task<Handcraft> ChangeHandicraft(Handcraft handcraft);

    IAsyncResult PushMailToHandicraft();
}