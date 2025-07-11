using FODUN.Reservas.Data;
using FODUN.Reservas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using FODUN.Reservas.Services; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
.AddRoles<IdentityRole<int>>() 
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IReservaService, ReservaService>();



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//  REGISTRO DE EMAIL SENDER Y CONFIGURACI�N DE EMAIL SETTINGS 

// Configurar EmailSettings desde appsettings.json
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Registrar la implementaci�n de IEmailSender
builder.Services.AddTransient<IEmailSender, EmailSender>();

// =====================================================================================


var app = builder.Build();

// L�gica de Seeding de Roles y Usuario Administrador al inicio de la aplicaci�n
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

        // Llamamos a la funci�n que crea el usuario administrador con valores directos
        await CreateAdminUser(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocurri� un error al inicializar la base de datos con los roles y el usuario administrador.");
        Console.WriteLine($"Error al inicializar roles y usuarios: {ex.Message}");
    }
}
// =====================================================================================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

// Funci�n para crear roles y usuario administrador 
async Task CreateAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager) // Aseg�rate de IdentityRole<int>
{
    // Crear roles si no existen
    string[] roleNames = { "Administrador", "Usuario" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(roleName)); 
            Console.WriteLine($"Rol '{roleName}' creado.");
        }
        else
        {
            Console.WriteLine($"Rol '{roleName}' ya existe.");
        }
    }

    // Crear usuario administrador si no existe
    var adminUserEmail = "admin@fodun.com"; 
    var adminPassword = "Adm1n_2025"; 
                                             

    var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminUserEmail,
            Email = adminUserEmail,
            EmailConfirmed = true,
            NumeroDocumento = "1000000000",
            NombreCompleto = "Administrador del Sistema",
            FechaNacimiento = new DateTime(1990, 1, 1),
            Celular = "3000000000",
            DireccionResidencia = "Calle Falsa 123",
            Barrio = "Centro",
            Municipio = "Medell�n",
            Departamento = "Antioquia",
            TelefonoResidencia = "0000000",
            PreguntaSecreta = "Mi color favorito",
            RespuestaSecreta = "Azul",
            AutorizaEnvioInfoCelular = false,
            AutorizaEnvioInfoCorreo = false,
            FechaCreacion = DateTime.UtcNow,
            FechaActualizacion = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Administrador");
            Console.WriteLine($"Usuario administrador '{adminUserEmail}' creado y asignado al rol 'Administrador'.");
        }
        else
        {
            Console.WriteLine($"Error al crear usuario administrador: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
    else
    {
        Console.WriteLine($"Usuario administrador '{adminUserEmail}' ya existe.");
        if (!await userManager.IsInRoleAsync(adminUser, "Administrador"))
        {
            await userManager.AddToRoleAsync(adminUser, "Administrador");
            Console.WriteLine($"Rol 'Administrador' asignado al usuario '{adminUserEmail}'.");
        }
    }
}