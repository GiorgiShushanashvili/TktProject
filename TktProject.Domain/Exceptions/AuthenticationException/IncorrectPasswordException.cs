namespace TktProject.Domain.Exceptions.AuthenticationException;

public class IncorrectPasswordException:Exception
{
    public IncorrectPasswordException():base("InCorrect Password,Try Again"){}
}