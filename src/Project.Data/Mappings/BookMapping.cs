using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Mappings
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(b => b.Resume)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(b => b.Image)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(b => b.Author)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(b => b.Volume)
                .HasColumnType("int");

            builder.Property(b => b.Publisher)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.ToTable("Books");
        }
    }
}
