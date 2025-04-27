using Microsoft.EntityFrameworkCore;
using TuncayAlbayrakMvcSinav.Data;
using TuncayAlbayrakMvcSinav.MapperClasses;
using TuncayAlbayrakMvcSinav.Models.Entities;
using TuncayAlbayrakMvcSinav.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//—- ConnectingString —-
string strConn = builder.Configuration.GetConnectionString("ConnStr");
builder.Services.AddDbContext<FilmlerDbContext>(x => x.UseSqlServer(strConn));

//—- Indetity —-
builder.Services.AddDefaultIdentity<Yonetmen>(x => x.SignIn.RequireConfirmedAccount = false)
.AddEntityFrameworkStores<FilmlerDbContext>();

//Mapper
builder.Services.AddAutoMapper(typeof(ProjectMapper));
builder.Services.AddScoped<FilmRepository>();
builder.Services.AddScoped<OyuncuRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
