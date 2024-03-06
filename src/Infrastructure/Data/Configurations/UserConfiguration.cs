//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Infrastructure.Data.Configurations;

//public class UserConfiguration : IEntityTypeConfiguration<Account>
//{
//    public void Configure(EntityTypeBuilder<Account> builder)
//    {
//        builder.ToTable("Accounts");

//        //Primary Key
//        builder.HasKey(t => t.Id);

//        //Properties
//        builder.Property(t => t.AccountNo).ValueGeneratedOnAdd();

//        builder.Property(t => t.RowVersion)
//            .IsRowVersion();
//    }
//}
