using DataAccess.Interface;
using DataAccess.models;
using Domain.Models.Users;

namespace DataAccess.Realization;

public class HandcraftRepositoryRepository : IHandcraftRepository
{
    public Task<Handcraft> GetHandicraft(UserAuthentication user)
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft[]> GetAllHandicraft()
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft[]> GetHandicraftToCity(string city)
    {
        throw new NotImplementedException();
    }

    public IAsyncResult AddHandicraft(Handcraft handcraft)
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft> ChangeHandicraft(Handcraft handcraft)
    {
        throw new NotImplementedException();
    }

    public IAsyncResult PushMailToHandicraft()
    {
        throw new NotImplementedException();
    }
}