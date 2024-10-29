using KlubSportowy.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using KlubSportowy.Models;
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
    }

public DbSet<KlubSportowy.Models.MeczModel> MeczModel { get; set; } = default!;

public DbSet<KlubSportowy.Models.StatystykiZawodnikaMeczModel> StatystykiZawodnikaMeczModel { get; set; } = default!;

public DbSet<KlubSportowy.Models.ZawodnikModel> ZawodnikModel { get; set; } = default!;
}



