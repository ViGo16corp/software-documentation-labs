using Microsoft.EntityFrameworkCore;
using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();
    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAccount>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Code)
            .IsUnique();

        modelBuilder.Entity<Card>()
            .HasIndex(c => c.Token)
            .IsUnique();

        modelBuilder.Entity<PaymentMethod>()
            .HasDiscriminator<string>("PaymentMethodType")
            .HasValue<Card>("Card");
    }
}
