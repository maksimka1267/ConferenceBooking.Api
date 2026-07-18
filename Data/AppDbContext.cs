using ConferenceBooking.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<ConferenceHall> ConferenceHalls => Set<ConferenceHall>();
    public DbSet<AdditionalService> AdditionalServices => Set<AdditionalService>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingService> BookingServices => Set<BookingService>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureConferenceHall(modelBuilder);
        ConfigureAdditionalService(modelBuilder);
        ConfigureBooking(modelBuilder);
        ConfigureBookedService(modelBuilder);
    }

    private static void ConfigureConferenceHall(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConferenceHall>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Capacity)
                .IsRequired();

            entity.Property(x => x.HourlyRate)
                .HasColumnType("decimal(18,2)");

            entity.HasMany(x => x.AdditionalServices)
                .WithOne(x => x.ConferenceHall)
                .HasForeignKey(x => x.ConferenceHallId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(x => x.Bookings)
                .WithOne(x => x.ConferenceHall)
                .HasForeignKey(x => x.ConferenceHallId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureAdditionalService(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalService>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");
        });
    }

    private static void ConfigureBooking(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.StartTime).IsRequired();

            entity.Property(x => x.EndTime).IsRequired();

            entity.Property(x => x.TotalPrice)
                .HasColumnType("decimal(18,2)");

            entity.HasMany(x => x.BookedServices)
                .WithOne(x => x.Booking)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureBookedService(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingService>(entity =>
        {
            entity.HasKey(x => new { x.BookingId, x.ServiceId });

            entity.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            entity.HasOne(x => x.Booking)
                .WithMany(x => x.BookedServices)
                .HasForeignKey(x => x.BookingId);

            entity.HasOne(x => x.Service)
                .WithMany(x => x.BookingServices)
                .HasForeignKey(x => x.ServiceId);
        });
    }
}