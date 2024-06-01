using MediatR;
using TktProject.Domain.DTOs.Sender;

namespace TktProject.App.API.Authentication;

public record RegistrationCommand(UserProfileDTO UserProfileDto):IRequest<Unit>;