using MediatR;
using TktProject.Domain.Entities;
using TktProject.Domain.Exceptions.AuthenticationException;
using TktProject.Infrastructure.Contracts;

namespace TktProject.App.API;

public class RegistrationCommandHandler:IRequestHandler<RegistrationCommand,Unit>
{
    private readonly IUnitOfWork<ITktDbContext> _unitOfWork;
    private IPasswordManagement _passwordManagement;

    public RegistrationCommandHandler(IUnitOfWork<ITktDbContext> unitOfWork, IPasswordManagement passwordManagement)
    {
        _unitOfWork = unitOfWork;
        _passwordManagement = passwordManagement;
    }

    public async Task<Unit> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var check =await _unitOfWork.GetRepository<UserProfile>()?
            .FindAsync(x => x.UserName == request.UserProfileDto.UserName);
        if (check != null)
            throw new UserNameAlreadyExistsException();

        if (request.UserProfileDto.Password != request.UserProfileDto.PasswordConfirmation)
            throw new ArgumentException("Check Your Password");
        var salt = _passwordManagement.CreatePassworSalt();
        var passHash = _passwordManagement.GetPasswordHash(request.UserProfileDto.Password, salt);
        var newUser=new User()
        {
            FirstName = request.UserProfileDto.FirstName,
            LastName = request.UserProfileDto.LastName,
            PersonalNumber = request.UserProfileDto.PersonalNumber,
            PhoneNumber = request.UserProfileDto.PhoneNumber,
            Email = request.UserProfileDto.Email,
            
        }

    }
}