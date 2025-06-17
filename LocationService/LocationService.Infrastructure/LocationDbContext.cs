using Microsoft.EntityFrameworkCore;
using LocationService.Domain;

namespace LocationService.Infrastructure;

public class LocationDbContext : DbContext
{
    public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options)
    {
    }

    public DbSet<Location> Locations { get; set; }
    public DbSet<LocationDetail> LocationDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("locations");
            
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();
            
            entity.Property(e => e.Address)
                .HasColumnName("address")
                .HasMaxLength(500)
                .IsRequired();
            
            entity.Property(e => e.Latitude)
                .HasColumnName("latitude")
                .HasPrecision(10, 8)
                .IsRequired();
            
            entity.Property(e => e.Longitude)
                .HasColumnName("longitude")
                .HasPrecision(11, 8)
                .IsRequired();
            
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            
            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at");
            
            entity.HasMany(e => e.Details)
                .WithOne(d => d.Location)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => new { e.Latitude, e.Longitude })
                .HasDatabaseName("idx_location_coordinates");
            
            entity.HasIndex(e => e.Address)
                .HasDatabaseName("idx_location_address");
        });

        modelBuilder.Entity<LocationDetail>(entity =>
        {
            entity.ToTable("location_details");
            
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();
            
            entity.Property(e => e.LocationId)
                .HasColumnName("location_id")
                .IsRequired();
            
            entity.Property(e => e.PropertyName)
                .HasColumnName("property_name")
                .HasMaxLength(100)
                .IsRequired();
            
            entity.Property(e => e.PropertyValue)
                .HasColumnName("property_value")
                .HasMaxLength(1000)
                .IsRequired();
            
            entity.HasIndex(e => e.PropertyName)
                .HasDatabaseName("idx_location_detail_property_name");
            
            entity.HasIndex(e => new { e.PropertyName, e.PropertyValue })
                .HasDatabaseName("idx_location_detail_name_value");
        });
    }
}