namespace TktProject.Domain.Exceptions.AuthenticationException;

public class UserNotFoundException:Exception
{
    public UserNotFoundException():base("Invalid Username"){}
}