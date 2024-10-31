using KlubSportowy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using KlubSportowy.Models;
using KlubSportowy.Models.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        modelBuilder.Entity<StatystykiZawodnikaMeczModel>()
            .HasOne(s => s.MeczModel)
            .WithMany(m => m.StatystykiZawodnikow)
            .HasForeignKey(s => s.MeczModelId);

        modelBuilder.Entity<StatystykiZawodnikaMeczModel>()
            .HasOne(s => s.ZawodnikModel)
            .WithMany(z => z.StatystykiZawodnikaZMeczu)
            .HasForeignKey(s => s.ZawodnikModelId);


        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.ZawodnikModel)
            .WithOne(p => p.ApplicationUser)
            .HasForeignKey<ZawodnikModel>(p => p.ApplicationUserId);


        modelBuilder.Entity<ZawodnikOgloszenie>()
    .HasKey(zo => new { zo.ZawodnikId, zo.OgloszenieId });

        modelBuilder.Entity<ZawodnikOgloszenie>()
            .HasOne(zo => zo.Zawodnik)
            .WithMany(u => u.ZawodnikOgloszenie) // Dodane powiązanie do kolekcji w ApplicationUser
            .HasForeignKey(zo => zo.ZawodnikId);

        modelBuilder.Entity<ZawodnikOgloszenie>()
            .HasOne(zo => zo.Ogloszenie)
            .WithMany(o => o.ZawodnikOgloszenie)
            .HasForeignKey(zo => zo.OgloszenieId);

    }

    public DbSet<KlubSportowy.Models.MeczModel> MeczModel { get; set; } = default!;

    public DbSet<KlubSportowy.Models.StatystykiZawodnikaMeczModel> StatystykiZawodnikaMeczModel { get; set; } = default!;

    public DbSet<KlubSportowy.Models.ZawodnikModel> ZawodnikModel { get; set; } = default!;

    public DbSet<KlubSportowy.Models.OgloszeniaModel> OgloszeniaModel { get; set; } = default!;
    public async Task RemoveExpiredOgloszeniaAsync()
    {
        var expiredOgloszenia = await OgloszeniaModel
            .Where(o => o.DataUsunieciaOgloszenia < DateTime.Now)
            .ToListAsync();

        OgloszeniaModel.RemoveRange(expiredOgloszenia);
        await SaveChangesAsync();
    }


    public DbSet<KlubSportowy.Models.ZawodnikOgloszenie> ZawodnikOgloszenie { get; set; }

}




