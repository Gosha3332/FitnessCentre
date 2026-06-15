using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Serviсe> Services { get; set; }
    public DbSet<ClientServiсe> ClientServiсes { get; set; }
    public DbSet<Locker> Lockers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasKey(cs => new { cs.Id});
        modelBuilder.Entity<ClientServiсe>()
            .HasKey(cs => new { cs.ClientId, cs.ServiseId });
        modelBuilder.Entity<Serviсe>()
            .HasKey(cs => new { cs.Id });
        modelBuilder.Entity<Locker>()
            .HasKey(cs => new { cs.Id });
        modelBuilder.Entity<Trainer>()
            .HasKey(cs => new { cs.Id });
    }
}