using Target10._9.Business.Users.Entities;

namespace Target10._9.Business.Users.Repositories;

public interface IUsersRepository
{
    Task<bool> CheckIfEmailExistsAsync(string email, CancellationToken cancellationToken);
    Task<User> AddUserAsync(User user, CancellationToken cancellationToken);
}