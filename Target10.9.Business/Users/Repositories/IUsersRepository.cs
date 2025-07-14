using Target10._9.Business.Users.Entities;

namespace Target10._9.Business.Users.Repositories;

public interface IUsersRepository
{
    #region Check

    Task<bool> CheckIfEmailExistsAsync(string email, CancellationToken cancellationToken);
    
    #endregion
    
    #region Get

    Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        
    #endregion
    
    #region Add
    
    Task<User> AddUserAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        string licenseNumber,
        CancellationToken cancellationToken
    );
    
    #endregion
    
    #region Update

    Task<User> UpdateUserAsync(
        Guid userId,
        string email,
        string firstName,
        string lastName,
        string licenseNumber,
        CancellationToken cancellationToken
    );

    #endregion
    
    #region Delete
    
    Task DeleteUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    
    #endregion
}