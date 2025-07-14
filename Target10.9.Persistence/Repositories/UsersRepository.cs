using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Users.Entities;
using Target10._9.Business.Users.Repositories;
using Target10._9.Persistence;

namespace Target10._9.Persistencence.Repositories;

public class UsersRepository (ApplicationDbContext dbContext) : IUsersRepository
{
    #region Check

    public Task<bool> CheckIfEmailExistsAsync(string email, CancellationToken cancellationToken)
    {  
        return dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
    
    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        user = new()
        {
            Email = user.Email,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            LicenseNumber = user.LicenseNumber
        };
        dbContext.Users.Add(user);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    #endregion
}