using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TktProject.Domain.Entities;

namespace TktProject.Persistance.EntityConfig;

public class TicketConfiguration:IEntityTypeConfiguration<Tickets>
{
    public void Configure(EntityTypeBuilder<Tickets> builder)
    {
        builder.ToTable("Tickets");
        builder.HasKey(x => x.Id);
        builder.OwnsOne(t => t.Price, price =>
        {
            price.Property(x => x.Amount).IsRequired();
            price.Property(x => x.Currency).IsRequired();
        });
        builder.Property(x=>x.Category).IsRequired();
        builder.Property(x=>x.Entrance).IsRequired();
        builder.Property(x=>x.Tier).IsRequired();
        builder.OwnsOne(x => x.SeatCoordinates, seat =>
        {
            seat.Property(x => x.Row).IsRequired();
            seat.Property(x => x.Sector).IsRequired();
            seat.Property(x => x.SeatNumber).IsRequired();
        });
        builder.Property(x=>x.OwnersFirstName).IsRequired();
        builder.Property(x=>x.OwnersLastName).IsRequired();
        builder.Property(x=>x.OwnersEmail).IsRequired();
        builder.Property(x=>x.OwnersPersonalNumber).IsRequired();
        builder.Property(x=>x.OwnersPhoneNumber).IsRequired();
        builder.HasOne(x => x.User).WithMany(c => c.Tickets);
    }
}
