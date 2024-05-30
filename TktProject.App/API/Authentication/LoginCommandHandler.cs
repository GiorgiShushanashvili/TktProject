using MediatR;
using TktProject.Domain.Entities;
using TktProject.Domain.Enums;
using TktProject.Domain.Exceptions;
using TktProject.Domain.Exceptions.AuthenticationException;
using TktProject.Infrastructure;
using TktProject.Infrastructure.Contracts;
using TktProject.Infrastructure.Repositories;

namespace TktProject.App.API.Authentication;

public class LoginCommandHandler:IRequestHandler<LoginCommand,string>
{
    private IUnitOfWork<ITktDbContext> _unitOfWork;
    private readonly IPasswordManagement _passwordManagement;
    private readonly ITokenManagement _tokenManagement;

    public LoginCommandHandler(IUnitOfWork<ITktDbContext> unitOfWork,IPasswordManagement passwordManagement,ITokenManagement tokenManagement)
    {
        _unitOfWork = unitOfWork;
        _passwordManagement = passwordManagement;
        _tokenManagement = tokenManagement;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userInfo =await _unitOfWork.GetRepository<UserProfile>().FindAsync(x => x.UserName == request.UserName)!;
        if (userInfo == null) throw new UserNotFoundException();
        if (userInfo.Status == Status.InActive)
            throw new ArgumentException("Your Account Is InActive");
        var userRole = await _unitOfWork.GetRepository<Roles>().GetByIdAsync(userInfo.RoleId)!;
        var passwordHash = _passwordManagement.GetPasswordHash(request.Password, userInfo.PasswordSalt);
        var checkPassword = _passwordManagement.IsValidPasswordHash(userInfo.PasswordHash, passwordHash);
        if (!checkPassword)
            throw new IncorrectPasswordException();
        var token =await _tokenManagement.CreateToken(userInfo.Id, userRole.ToString());
        return token;
    }
}