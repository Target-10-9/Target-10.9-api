using System.ComponentModel.DataAnnotations;

namespace Target10._9.Business.Accounts.Responses;

public class RegisterResponse
{
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Message { get; set; } = null!;
}