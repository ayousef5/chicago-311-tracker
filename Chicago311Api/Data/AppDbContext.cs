using Chicago311Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Chicago311Api.Data;

// Connects our C# application to the MySQL database.
public class AppDbContext : DbContext
{
    // Constructor used by ASP.NET Core.
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // Represents the service_requests table.
    public DbSet<ServiceRequest> ServiceRequests { get; set; }

    // Configures how the C# model maps to MySQL.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tell EF Core which MySQL table to use.
        modelBuilder.Entity<ServiceRequest>()
            .ToTable("service_requests");

        // Tell EF Core that sr_number is the primary key.
        modelBuilder.Entity<ServiceRequest>()
            .HasKey(request => request.SrNumber);

        // Map the C# property SrNumber to sr_number.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.SrNumber)
            .HasColumnName("sr_number");

        // Map the C# property SrType to sr_type.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.SrType)
            .HasColumnName("sr_type");

        // Map the C# property SrShortCode to sr_short_code.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.SrShortCode)
            .HasColumnName("sr_short_code");

        // Map the C# property OwnerDepartment to owner_department.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.OwnerDepartment)
            .HasColumnName("owner_department");

        // Map the C# property Status to status.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.Status)
            .HasColumnName("status");

        // Map the C# property Origin to origin.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.Origin)
            .HasColumnName("origin");

        // Map the C# property CreatedDate to created_date.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.CreatedDate)
            .HasColumnName("created_date");

        // Map the C# property LastModifiedDate to last_modified_date.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.LastModifiedDate)
            .HasColumnName("last_modified_date");

        // Map the C# property ClosedDate to closed_date.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.ClosedDate)
            .HasColumnName("closed_date");

        // Map the C# property StreetAddress to street_address.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.StreetAddress)
            .HasColumnName("street_address");

        // Map the C# property City to city.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.City)
            .HasColumnName("city");

        // Map the C# property State to state.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.State)
            .HasColumnName("state");

        // Map the C# property ZipCode to zip_code.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.ZipCode)
            .HasColumnName("zip_code");

        // Map the C# property CommunityArea to community_area.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.CommunityArea)
            .HasColumnName("community_area");

        // Map the C# property Ward to ward.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.Ward)
            .HasColumnName("ward");

        // Map the C# property Latitude to latitude.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.Latitude)
            .HasColumnName("latitude");

        // Map the C# property Longitude to longitude.
        modelBuilder.Entity<ServiceRequest>()
            .Property(request => request.Longitude)
            .HasColumnName("longitude");
    }
}