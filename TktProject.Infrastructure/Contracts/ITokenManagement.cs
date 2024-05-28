namespace TktProject.Infrastructure.Contracts;

public interface ITokenManagement
{
    public Task<string> CreateToken(int userId, string role);
}