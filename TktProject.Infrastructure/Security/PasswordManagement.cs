using TktProject.Infrastructure.Contracts;

namespace TktProject.Infrastructure.Security;

public class PasswordManagement:IPasswordManagement
{
    public byte[] GetPasswordHash(string password, byte[] passwordSalt)
    {
        throw new NotImplementedException();
    }

    public byte[] CreatePassworSalt()
    {
        throw new NotImplementedException();
    }

    public bool IsValidPasswordHash(byte[] userPasswordHash, byte[] generatedPasswordHash)
    {
        throw new NotImplementedException();
    }
}