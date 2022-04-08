using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Business.Models;

namespace Project.Data.Mappings
{
    public  class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(s => s.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            //1 : 1 -> Relation Supplier: Address
            builder.HasOne(s => s.Address)
                .WithOne(a => a.Supplier);

            //1 : N -> Relation Supplier: Books
            builder.HasMany(s => s.Books)
                .WithOne(b => b.Supplier)
                .HasForeignKey(b => b.SupplierId);

            builder.ToTable("Suppliers");
        }
    }
}
