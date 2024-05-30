namespace TktProject.Domain.Exceptions.AuthenticationException;

public class UserNameAlreadyExistsException:Exception
{
    public UserNameAlreadyExistsException():base("UserName Already Used"){}
}