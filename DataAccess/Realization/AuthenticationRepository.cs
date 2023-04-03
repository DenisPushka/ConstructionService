using DataAccess.Interface;
using DataAccess.models;

namespace DataAccess.Realization;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly DataSql _context;

    public AuthenticationRepository(DataSql context) => _context = context;

    public async Task<bool> Authentication(UserAuthentication userAuthentication)
    {
        return await _context.Authentication(userAuthentication);
    }
}