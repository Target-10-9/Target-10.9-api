using Microsoft.Extensions.Configuration;

namespace Target10._9.Persistence;

public interface IDbConfiguration
{
    string ConnectionString { get; }
}

public class DefaultDbConfiguration(IConfiguration config) : IDbConfiguration
{
    public string ConnectionString => config.GetConnectionString(Constants.ConnectionString)!;
}