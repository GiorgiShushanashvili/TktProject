namespace TktProject.Infrastructure.Contracts;

public interface IPasswordManagement
{
    public byte[] GetPasswordHash(string password, byte[] passwordSalt);
    public byte[] CreatePassworSalt();
    bool IsValidPasswordHash(byte[] userPasswordHash, byte[] generatedPasswordHash);
}