using MediatR;
using TktProject.Domain.DTOs.Sender;

namespace TktProject.App.API;

public record RegistrationCommand(UserProfileDTO UserProfileDto):IRequest<Unit>;