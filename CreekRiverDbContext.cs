using Microsoft.EntityFrameworkCore;
using CreekRiver.Models;

public class CreekRiverDbContext : DbContext
{
    // DbSets represent the tables in the database
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Campsite> Campsites { get; set; }
    public DbSet<CampsiteType> CampsiteTypes { get; set; }

    // Constructor that passes the DbContextOptions to the base class
    public CreekRiverDbContext(DbContextOptions<CreekRiverDbContext> context) : base(context)
    {
    }

    // Method to seed data to the database when it is created or updated
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data for CampsiteType
        modelBuilder.Entity<CampsiteType>().HasData(new CampsiteType[]
        {
            new CampsiteType {Id = 1, CampsiteTypeName = "Tent", FeePerNight = 15.99M, MaxReservationDays = 7},
            new CampsiteType {Id = 2, CampsiteTypeName = "RV", FeePerNight = 26.50M, MaxReservationDays = 14},
            new CampsiteType {Id = 3, CampsiteTypeName = "Primitive", FeePerNight = 10.00M, MaxReservationDays = 3},
            new CampsiteType {Id = 4, CampsiteTypeName = "Hammock", FeePerNight = 12M, MaxReservationDays = 7}
        });

        // Seed data for Campsites
        modelBuilder.Entity<Campsite>().HasData(new Campsite[]
        {
            new Campsite {Id = 1, CampsiteTypeId = 1, Nickname = "Barred Owl", ImageUrl = "https://tnstateparks.com/assets/images/content-images/campgrounds/249/colsp-area2-site73.jpg"},
            new Campsite {Id = 2, CampsiteTypeId = 2, Nickname = "Eagle's Nest", ImageUrl = "https://example.com/eagle-nest.jpg"},
            new Campsite {Id = 3, CampsiteTypeId = 3, Nickname = "Bear Cave", ImageUrl = "https://example.com/bear-cave.jpg"},
            new Campsite {Id = 4, CampsiteTypeId = 4, Nickname = "Cedar Grove", ImageUrl = "https://example.com/cedar-grove.jpg"},
            new Campsite {Id = 5, CampsiteTypeId = 1, Nickname = "Deer Run", ImageUrl = "https://example.com/deer-run.jpg"},
            new Campsite {Id = 6, CampsiteTypeId = 2, Nickname = "River Bend", ImageUrl = "https://example.com/river-bend.jpg"}
        });

        // Seed data for UserProfiles
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile[]
        {
            new UserProfile {Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com"},
            new UserProfile {Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com"}
        });

        // Seed data for Reservations
        modelBuilder.Entity<Reservation>().HasData(new Reservation[]
        {
            new Reservation {Id = 1, CampsiteId = 1, UserProfileId = 1, CheckinDate = DateTime.Parse("2024-06-01"), CheckoutDate = DateTime.Parse("2024-06-07")},
            new Reservation {Id = 2, CampsiteId = 3, UserProfileId = 2, CheckinDate = DateTime.Parse("2024-06-10"), CheckoutDate = DateTime.Parse("2024-06-12")}
        });
    }
}
