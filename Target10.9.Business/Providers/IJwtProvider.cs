namespace Target10._9.Business.Providers;

public interface IJwtProvider
{
    string GenerateToken(string userId, string email);
}