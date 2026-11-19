using LibraryOfThingsM4.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryOfThingsM4.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Donor> Donors => Set<Donor>();

    public DbSet<Item> Items => Set<Item>();

    public DbSet<Waitlist> Waitlist => Set<Waitlist>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donor>(entity =>
        {
            entity.ToTable("Donor", "dbo");
            entity.HasKey(donor => donor.DonorId);
            entity.Property(donor => donor.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Items", "dbo");
            entity.HasKey(item => item.ItemId);
            entity.Property(item => item.ItemId)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEXT VALUE FOR dbo.Items_ItemID_Sequence");
            entity.Property(item => item.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(item => item.SerialNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(item => item.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(item => item.ReplacementValue)
                .HasPrecision(10, 2);
            entity.HasOne<Donor>()
                .WithMany()
                .HasForeignKey(item => item.DonorId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Waitlist>(entity =>
        {
            entity.ToTable("Waitlist", "dbo");
            entity.HasKey(waitlist => new { waitlist.ItemId, waitlist.MemberId });
            entity.HasOne<Item>()
                .WithMany()
                .HasForeignKey(waitlist => waitlist.ItemId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
