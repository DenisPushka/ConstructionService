using DataAccess.models;

namespace DataAccess.Interface;

public interface IAuthenticationRepository
{ 
    Task<char> Authentication(UserAuthentication userAuthentication);
}