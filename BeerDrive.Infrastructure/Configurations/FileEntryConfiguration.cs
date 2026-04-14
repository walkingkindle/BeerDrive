using BeerDrive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeerDrive.Infrastructure.Configurations
{
    public class FileEntryConfiguration : IEntityTypeConfiguration<FileEntry>
    {
        public void Configure(EntityTypeBuilder<FileEntry> builder)
        {
            builder.ToTable("FileEntries");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Name)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(255);

                name.Property(n => n.Extension)
                    .HasColumnName("Extension")
                    .IsRequired()
                    .HasMaxLength(10);
            });

            builder.OwnsOne(x => x.Size, size =>
            {
                size.Property(s => s.SizeValue)
                    .HasColumnName("SizeValue")
                    .IsRequired();

                size.Property(s => s.Unit)
                    .HasColumnName("Unit")
                    .IsRequired()
                    .HasMaxLength(10);
            });

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.Navigation(x => x.Name).IsRequired();
            builder.Navigation(x => x.Size).IsRequired();
        }
    }
}
