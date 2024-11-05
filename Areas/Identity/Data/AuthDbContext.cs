using KlubSportowy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KlubSportowy.Models;

namespace KlubSportowy.Data;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacje dla Statystyki Zawodnika Mecz
        modelBuilder.Entity<StatystykiZawodnikaMeczModel>()
            .HasOne(s => s.MeczModel)
            .WithMany(m => m.StatystykiZawodnikow)
            .HasForeignKey(s => s.MeczModelId);

        modelBuilder.Entity<StatystykiZawodnikaMeczModel>()
            .HasOne(s => s.ZawodnikModel)
            .WithMany(z => z.StatystykiZawodnikaZMeczu)
            .HasForeignKey(s => s.ZawodnikModelId);

        // Relacja pomiędzy ApplicationUser a ZawodnikModel
        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.ZawodnikModel)
            .WithOne(z => z.ApplicationUser)
            .HasForeignKey<ZawodnikModel>(z => z.ApplicationUserId);

        // Klucz złożony dla ZawodnikOgloszenie
        modelBuilder.Entity<ZawodnikOgloszenie>()
            .HasKey(zo => new { zo.ZawodnikId, zo.OgloszenieId });

        // Relacja pomiędzy ZawodnikOgloszenie a Zawodnik
        modelBuilder.Entity<ZawodnikOgloszenie>()
            .HasOne(zo => zo.Zawodnik)
            .WithMany(z => z.ZawodnikOgloszenie)
            .HasForeignKey(zo => zo.ZawodnikId);

        // Relacja pomiędzy ZawodnikOgloszenie a OgloszeniaModel
        modelBuilder.Entity<ZawodnikOgloszenie>()
            .HasOne(zo => zo.Ogloszenie)
            .WithMany(o => o.ZawodnikOgloszenie)
            .HasForeignKey(zo => zo.OgloszenieId);

        // Relacja pomiędzy ApplicationUser a ZawodnikOgloszenie
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.ZawodnikOgloszenie)
            .WithOne(zo => zo.Zawodnik)
            .HasForeignKey(zo => zo.ZawodnikId);

        modelBuilder.Entity<WydarzenieModel>()
                .HasOne(w => w.MeczModel)
                .WithOne(m => m.Wydarzenie)
                .HasForeignKey<WydarzenieModel>(w => w.MeczModelId);
    }

    // DbSet dla modeli
    public DbSet<MeczModel> MeczModel { get; set; } = default!;
    public DbSet<StatystykiZawodnikaMeczModel> StatystykiZawodnikaMeczModel { get; set; } = default!;
    public DbSet<ZawodnikModel> ZawodnikModel { get; set; } = default!;
    public DbSet<OgloszeniaModel> OgloszeniaModel { get; set; } = default!;
    public DbSet<ZawodnikOgloszenie> ZawodnikOgloszenie { get; set; } = default!;
    public DbSet<WydarzenieModel> WydarzenieModel { get; set; } = default!;

    public async Task RemoveExpiredOgloszeniaAsync()
    {
        var expiredOgloszenia = await OgloszeniaModel
            .Where(o => o.DataUsunieciaOgloszenia < DateTime.Now)
            .ToListAsync();

        OgloszeniaModel.RemoveRange(expiredOgloszenia);
        await SaveChangesAsync();
    }
}
