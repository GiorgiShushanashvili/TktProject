using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TktProject.Domain.Enums;
using TktProject.Domain.ValueObjects;

namespace TktProject.Domain.DTOs.Response;
public record TicketRS(Price Price,Category Category,Entrance Entrance,Tier Tier,SeatCoordinates SeatCoordinates);