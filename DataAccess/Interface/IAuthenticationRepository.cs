using DataAccess.models;

namespace DataAccess.Interface;

public interface IAuthenticationRepository
{ 
    Task<bool> Authentication(UserAuthentication userAuthentication);
}