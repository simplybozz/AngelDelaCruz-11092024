using EvaluacionTecnica.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configura los servicios de sesión
builder.Services.AddDistributedMemoryCache(); // Opcional: para cache en memoria
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Expiración de sesión
    options.Cookie.HttpOnly = true; // Solo HTTP
    options.Cookie.IsEssential = true; // Necesario para la app
});

// Configura autorización y autenticación
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Login"; // Ruta de inicio de sesión
        options.AccessDeniedPath = "/AccessDenied"; // Ruta de acceso denegado
    });

// Configura el DbContext con la cadena de conexión
builder.Services.AddDbContext<EvaluacionTecnicaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agrega soporte para controladores con vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura el middleware
app.UseHttpsRedirection(); // Redirige HTTP a HTTPS
app.UseStaticFiles(); // Sirve archivos estáticos
app.UseRouting(); // Rutea las solicitudes

// Configura el middleware de sesión
app.UseSession(); // Maneja sesiones

// Configura autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Configura las rutas de los controladores
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Ejecuta la aplicación