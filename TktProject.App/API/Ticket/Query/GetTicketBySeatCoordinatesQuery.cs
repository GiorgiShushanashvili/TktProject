using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TktProject.Domain.DTOs.Response;
using TktProject.Domain.ValueObjects;

namespace TktProject.App.API.Ticket.Query;
public record GetTicketBySeatCoordinatesQuery(SeatCoordinates SeatCoordinates):IRequest<TicketRS>;