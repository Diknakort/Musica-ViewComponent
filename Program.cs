using BaseDatosMusica.Models;
using BaseDatosMusica.Services;
using BaseDatosMusica.Services.Repositorio;
using BaseDatosMusica.Services.Specification;
using BaseDatosMusica.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GrupoDContext>(options =>
{
    options.UseSqlServer(builder.Configuration["\"server=musicagrupos.database.windows.net;database=GrupoD;user=as;password=P0t@t0P0t@t0\""],
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
});
//builder.Services.AddDbContext<GrupoDContext>(
//       options => options.UseSqlServer("\"server=musicagrupos.database.windows.net;database=GrupoD;user=as;password=P0t@t0P0t@t0\""));
builder.Services.AddScoped<ICrearListadoViewModel, CrearListadoViewModel>();
builder.Services.AddScoped<IDiscosSinCancionesBuilder, DiscosesSinCancionesBuilder>();
builder.Services.AddScoped<IGirasRepositorio, EFGirasRepositorio>();
builder.Services.AddScoped(typeof(IGenericRepositorio<>), typeof(EFGenericRepositorio<>));
builder.Services.AddScoped<IArtistaSpecification, ArtistaSpecification>();
builder.Services.AddScoped<IColaboracionesSpecification, ColaboradoresSpecification>();
builder.Services.AddScoped<IGrupoArtistaRolSpecification, GrupoArtistaRolSpecification>();
builder.Services.AddScoped<IGrupoSpecification, GrupoSpecification>();





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
