using MediatR;
using TktProject.Domain.Entities;
using TktProject.Infrastructure;
using TktProject.Infrastructure.Contracts;
using TktProject.Infrastructure.Repositories;

namespace TktProject.App.API.Authentication;

public class LoginCommandHandler:IRequestHandler<LoginCommand,string>
{
    private IUnitOfWork<ITktDbContext> _unitOfWork;

    public LoginCommandHandler(IUnitOfWork<ITktDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userInfo =await _unitOfWork.GetRepository<UserProfile>().FindAsync(x => x.UserName == request.UserName)!;
    }
}