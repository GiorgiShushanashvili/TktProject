namespace TktProject.Domain.Exceptions.Ticket;

public class TicketALreadySoldException:Exception
{
    public TicketALreadySoldException():base("This Ticket is Already Sold"){}
}