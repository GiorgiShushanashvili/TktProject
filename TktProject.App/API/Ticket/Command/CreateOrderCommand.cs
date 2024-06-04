using MediatR;
using TktProject.Domain.ValueObjects;

namespace TktProject.App.API.Ticket.Command;

public record CreateOrderCommand(List<SeatCoordinates> listOfCoordinates) : IRequest<Unit>;