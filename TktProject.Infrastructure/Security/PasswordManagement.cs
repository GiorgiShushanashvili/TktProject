using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using TktProject.Infrastructure.Contracts;

namespace TktProject.Infrastructure.Security;

public class PasswordManagement:IPasswordManagement
{
    private readonly IConfiguration _configuration;

    public PasswordManagement(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public byte[] GetPasswordHash(string password, byte[] passwordSalt)
    {
        var hashed = KeyDerivation.Pbkdf2(
            password: password,
            salt: passwordSalt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32);
        return hashed;
    }

    public byte[] CreatePassworSalt()
    {
        var size = new byte[16];
        using (RandomNumberGenerator rand=RandomNumberGenerator.Create())
        {
            rand.GetBytes(size);
        }

        return size;
    }

    public bool IsValidPasswordHash(byte[] userPasswordHash, byte[] generatedPasswordHash)
    {
        for (int i = 0; i < userPasswordHash.Length; i++)
        {
            if (generatedPasswordHash[i] != userPasswordHash[i])
                return false;
        }
        return true;
    }
}