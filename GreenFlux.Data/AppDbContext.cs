using Microsoft.EntityFrameworkCore;
using GreenFlux.Data.Models;

namespace GreenFlux.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<ChargeStation> ChargeStations { get; set; }
        public DbSet<Connector> Connectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<ChargeStation>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<ChargeStation>()
                .HasOne(c => c.Group)
                .WithMany(c => c.ChargeStationCollection)
                .HasForeignKey(c => c.GroupId);

            modelBuilder.Entity<Connector>()
                .HasKey(c => new { c.Id, c.ChargeStationId });

            modelBuilder.Entity<Connector>()
                .HasOne(c => c.ChargeStation)
                .WithMany(c => c.ConnectorCollection)
                .HasForeignKey(c => c.ChargeStationId)
                .IsRequired();
        }
    }
}
