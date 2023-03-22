using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
using SharedModels.Models.RatingEntities;

namespace DbProvider.Data;

public class CurrencyBattleDbContext : DbContext
{
    public CurrencyBattleDbContext(DbContextOptions<CurrencyBattleDbContext> options)
        : base(options) { }

    public DbSet<User>? TUsers { get; set; }

    public DbSet<Room>? TRooms { get; set; }

    public DbSet<TotalPlayed>? TotalPLayedRating { get; set; }

    public DbSet<TotalVictories>? TotalVictoriesRating { get; set; }

    public DbSet<Winrate>? WinrateiesRating { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        _ = modelBuilder
            .Entity<User>()
            .ToTable("t_users")
            .HasKey("Login");

        _ = modelBuilder
            .Entity<User>()
            .Property(x => x.Password)
            .IsRequired();

        _ = modelBuilder
            .Entity<User>()
            .Property(x => x.Balance)
            .HasDefaultValue(100);

        _ = modelBuilder
            .Entity<User>()
            .Property(x => x.Name)
            .IsRequired();

        _ = modelBuilder
            .Entity<User>()
            .Property(x => x.TotalGames)
            .HasDefaultValue(0);

        _ = modelBuilder
            .Entity<User>()
            .Property(x => x.Victories)
            .HasDefaultValue(0);

        _ = modelBuilder
            .Entity<User>()
            .Property(x => x.History)
            .HasDefaultValue(string.Empty);
        _ = modelBuilder
            .Entity<User>()
            .Property(x => x.Notification)
            .HasDefaultValue(string.Empty);

        _ = modelBuilder
            .Entity<Room>()
            .ToTable("t_rooms")
            .HasKey("RoomId");

        _ = modelBuilder
            .Entity<Room>()
            .Property(x => x.Currency)
            .IsRequired();

        _ = modelBuilder
            .Entity<Room>()
            .Property(x => x.Date)
            .IsRequired();

        _ = modelBuilder
            .Entity<Room>()
            .Property(x => x.Bets)
            .HasDefaultValue(string.Empty);

        _ = modelBuilder
            .Entity<TotalPlayed>()
            .ToTable("t_total_pLayed_rating")
            .HasKey("Login");

        _ = modelBuilder
            .Entity<TotalVictories>()
            .ToTable("t_total_victories_rating")
            .HasKey("Login");

        _ = modelBuilder
            .Entity<Winrate>()
            .ToTable("t_winrate_rating")
            .HasKey("Login");

        _ = modelBuilder
            .Entity<TotalPlayed>()
            .Property(x => x.Games)
            .IsRequired();

        _ = modelBuilder
            .Entity<TotalVictories>()
            .Property(x => x.Games)
            .IsRequired();

        _ = modelBuilder
            .Entity<Winrate>()
            .Property(x => x.Games)
            .IsRequired();

        _ = modelBuilder
            .Entity<TotalPlayed>()
            .Property(x => x.Name)
            .IsRequired();

        _ = modelBuilder
            .Entity<TotalVictories>()
            .Property(x => x.Name)
            .IsRequired();

        _ = modelBuilder
            .Entity<Winrate>()
            .Property(x => x.Name)
            .IsRequired();
    }
}
