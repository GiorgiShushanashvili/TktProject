using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TktProject.Domain.Entities;

namespace TktProject.Persistance.EntityConfig;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.FirstName).IsRequired();
        builder.Property(x=>x.LastName).IsRequired();
        builder.Property(x=>x.Email).IsRequired();
        builder.Property(x=> x.Password).IsRequired();
        builder.Property(x=>x.PersonalNumber).IsRequired();
        builder.Property(x=>x.PhoneNumber).IsRequired();
        builder.HasMany(x => x.Tickets).WithOne(x => x.User).HasForeignKey(x => x.UserId);
    }
}
