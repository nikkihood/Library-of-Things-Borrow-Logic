using Microsoft.EntityFrameworkCore;

public class LibraryOfThingsM4Context(DbContextOptions<LibraryOfThingsM4Context> options) : DbContext(options)
{
    public DbSet<LibraryOfThingsM4.Models.Donor> Donor { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LibraryOfThingsM4.Models.Donor>()
            .Property(donor => donor.DonorId)
            .ValueGeneratedNever();
    }
}
