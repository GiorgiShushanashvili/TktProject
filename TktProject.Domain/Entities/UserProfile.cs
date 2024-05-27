using TktProject.Domain.Enums;

namespace TktProject.Domain.Entities;

public class UserProfile
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    public required Status Status { get; set; }
    public int RoleId { get; set; }
    public User User { get; set; }
}