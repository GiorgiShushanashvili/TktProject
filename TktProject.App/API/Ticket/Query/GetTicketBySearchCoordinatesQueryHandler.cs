using MediatR;
using TktProject.Domain.DTOs.Response;
using TktProject.Infrastructure.Contracts;
using TktProject.Domain.Entities;

namespace TktProject.App.API.Ticket.Query;
public class GetTicketBySearchCoordinatesQueryHandler:IRequestHandler<GetTicketBySeatCoordinatesQuery,TicketRS>
{
    private readonly IUnitOfWork<ITktDbContext> _uitOfWork;
    public GetTicketBySearchCoordinatesQueryHandler(IUnitOfWork<ITktDbContext> unitOfWork)
    {
        _uitOfWork=unitOfWork;
    }
    public async Task<TicketRS> Handle(GetTicketBySeatCoordinatesQuery request,CancellationToken cancellationToken)
    {
        var ticket=await _uitOfWork.GetRepository<Tickets>().FindAsync(x=>x.SeatCoordinates.Row==request.SeatCoordinates.Row
        &&x.SeatCoordinates.Sector==request.SeatCoordinates.Sector&&x.SeatCoordinates.SeatNumber==request.SeatCoordinates.SeatNumber);
        if(ticket==null)
            throw new 
    }
}
