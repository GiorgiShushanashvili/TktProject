namespace TktProject.Domain.Entities;

public class Roles
{
    public int Id { get; set; }
    public required string Role { get; set; }
    public ICollection<UserProfile> UserProfiles { get; set; }
}