using MediatR;
using TktProject.Domain.Entities;
using TktProject.Infrastructure.Contracts;

namespace TktProject.App.API.Ticket.Command;

public class CreateOrderCommandHandler:IRequestHandler<CreateOrderCommand,Unit>
{
    private readonly IUnitOfWork<ITktDbContext> _unitOfWork;

    public CreateOrderCommandHandler(IUnitOfWork<ITktDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order();
        foreach (var seat in request.listOfCoordinates)
        {
            var ticket =await _unitOfWork.GetRepository<Tickets>().FindAsync(x=>x.SeatCoordinates.Row==seat.Row
                &&x.SeatCoordinates.Sector==seat.Sector&&x.SeatCoordinates.SeatNumber==seat.SeatNumber);
            if (ticket.IsEnabled && ticket != null)
            {
                ticket.IsEnabled = false;
                order.Tickets.Add(ticket);
                order.TicketsPrice = order.TicketsPrice + ticket.Price.Amount;
                order.ServiceCost = order.ServiceCost + 1;
            }
        }
        order.TotalCost = order.TicketsPrice + order.ServiceCost;
        await _unitOfWork.SaveAsync();
        return Unit.Value;
    }
}