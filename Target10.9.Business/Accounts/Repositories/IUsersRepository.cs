using Target10._9.Business.Accounts.Entities;

namespace Target10._9.Business.Accounts.Repositories;

public interface IUsersRepository
{
    Task<bool> CheckIfEmailExistsAsync(string email, CancellationToken cancellationToken);
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
}