using EvaluacionTecnica.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura los servicios de sesi�n
builder.Services.AddDistributedMemoryCache(); // Opcional: para cache en memoria
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Expiraci�n de sesi�n
    options.Cookie.HttpOnly = true; // Solo HTTP
    options.Cookie.IsEssential = true; // Necesario para la app
});

// Configura autorizaci�n y autenticaci�n
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Login"; // Ruta de inicio de sesi�n
        options.AccessDeniedPath = "/AccessDenied"; // Ruta de acceso denegado
    });

// Configura el DbContext con la cadena de conexi�n
builder.Services.AddDbContext<EvaluacionTecnicaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agrega soporte para controladores con vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura el middleware
app.UseHttpsRedirection(); // Redirige HTTP a HTTPS
app.UseStaticFiles(); // Sirve archivos est�ticos
app.UseRouting(); // Rutea las solicitudes

// Configura el middleware de sesi�n
app.UseSession(); // Maneja sesiones

// Configura autenticaci�n y autorizaci�n
app.UseAuthentication();
app.UseAuthorization();

// Configura las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Ejecuta la aplicaci�n