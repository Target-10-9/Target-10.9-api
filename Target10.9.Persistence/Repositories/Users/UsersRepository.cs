using Microsoft.EntityFrameworkCore;
using Target10._9.Business.Users.Entities;
using Target10._9.Business.Users.Repositories;

namespace Target10._9.Persistence.Repositories.Users;

public class UsersRepository (ApplicationDbContext dbContext) : IUsersRepository
{
    #region Check

    public Task<bool> CheckIfEmailExistsAsync(string email, CancellationToken cancellationToken)
    {  
        return dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }
    
    #endregion
    
    #region Get

    public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
    
    public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    #endregion
    
    #region Add
    public async Task<User> AddUserAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        string licenseNumber,
        CancellationToken cancellationToken
    )
    {
        User user;
        user = new()
        {
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
            LicenseNumber = licenseNumber
        };
        dbContext.Users.Add(user);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    #endregion
    
    #region Update
    
    public async Task<User> UpdateUserAsync(
        Guid userId,
        string email,
        string firstName,
        string lastName,
        string licenseNumber,
        CancellationToken cancellationToken
    )
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if (user is null)
            throw new InvalidOperationException("Utilisateur non trouvé");
        
        user.Email = email;
        user.FirstName = firstName;
        user.LastName = lastName;
        user.LicenseNumber = licenseNumber;

        dbContext.Users.Update(user);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }
    
    #endregion
    
    #region Delete
    
    public async Task DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if (user is null)
            throw new InvalidOperationException("Utilisateur non trouvé");
        
        dbContext.Users.Remove(user);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    #endregion
    
}