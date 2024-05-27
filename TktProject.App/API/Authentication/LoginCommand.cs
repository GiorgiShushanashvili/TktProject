using MediatR;
namespace TktProject.App.API.Authentication;

public record LoginCommand(string UserName,string Password):IRequest<string>;