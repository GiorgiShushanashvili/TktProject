namespace TktProject.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public string PersonalNumber {  get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<Tickets> Tickets { get; set; }
    public UserProfile UserProfile { get; set; }
    public int UserProfileId{get;set;}
}
