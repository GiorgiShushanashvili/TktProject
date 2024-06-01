using TktProject.Domain.Enums;
using TktProject.Domain.ValueObjects;

namespace TktProject.Domain.Entities;

public class Tickets
{
    public int Id { get; set; }
    public Price Price { get; set; }
    public Category Category { get; set; }
    public Entrance Entrance { get; set; }
    public Tier Tier { get; set; }
    public SeatCoordinates SeatCoordinates { get; set; }
    public string? OwnersFirstName {  get; set; }
    public string? OwnersLastName { get; set; }
    public string? OwnersPersonalNumber {  get; set; }
    public string? OwnersEmail { get; set; }
    public string? OwnersPhoneNumber { get; set; }
    public User? User { get; set; }
    public int? UserId {  get; set; }
}
