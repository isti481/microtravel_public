using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microtravel.Data;
using Microsoft.Extensions.DependencyInjection;
using Microtravel.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MicrotravelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MicrotravelContext") ?? throw new InvalidOperationException("Connection string 'MicrotravelContext' not found.")));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// CORS enable
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", policy =>
    {
        //all
        //policy.AllowAnyOrigin()
        //restricted
        policy.WithOrigins(
                "https://isti481.github.io",
                "http://localhost:8080"
            )
            .AllowAnyHeader()    // all header 
            .AllowAnyMethod();   // all HTTP method 
    });
});

builder.Services.AddControllers(); // ha Web API-t használsz

// CORS end

// custom validators off 
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    // email confirmation 
    .AddRoles<IdentityRole>() // sajat
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddPasswordValidator<CustomPasswordValidator>(); // custom password validator add 
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// CORS
app.UseCors("MyCorsPolicy");

// CORS END

app.UseAuthorization();

// CORS

app.MapControllers();
// CORS END

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
