namespace TktProject.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId {  get; set; }
    public decimal TicketsPrice {  get; set; }
    public decimal TransactionCost {  get; set; }
    public decimal ServiceCost {  get; set; }
    public decimal TotalCost { get; set; }
    public List<Tickets> Tickets { get; set; }
}
