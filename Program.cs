using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KlubSportowy.Data;
using KlubSportowy.Areas.Identity.Data;
using KlubSportowy.Services; 
using Microsoft.AspNetCore.Identity.UI.Services;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

builder.Services.AddDbContext<AuthDbContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("AuthDbContextConnection"),
    new MySqlServerVersion(new Version(10, 6, 16))
));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure password options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

// Pobierz konfiguracj� SMTP z appsettings.json
var emailConfig = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
builder.Services.AddSingleton<IEmailSender>(new EmailSender(
    emailConfig.SmtpHost,
    emailConfig.SmtpPort,
    emailConfig.SmtpUsername,
    emailConfig.SmtpPassword
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

