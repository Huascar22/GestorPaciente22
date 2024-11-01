
using Controlador;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Modelo.AppContext;
using Modelo.Entidades;
using Modelo.Interfaces;
using Modelo.Repositorio;
using Vista.Controllers;
using Vista.Views.Shared.Servicios;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ServiciosView>();
builder.Services.AddScoped<IRepositorio<Medico>, Repositorio<Medico>>();
builder.Services.AddScoped<IServicios<Medico>, Servicios<Medico>>();
builder.Services.AddScoped<IRepositorio<Cita>, Repositorio<Cita>>();
builder.Services.AddScoped<IServicios<Cita>, Servicios<Cita>>();
builder.Services.AddScoped<IRepositorio<Paciente>, Repositorio<Paciente>>();
builder.Services.AddScoped<IServicios<Paciente>, Servicios<Paciente>>();
builder.Services.AddScoped<IRepositorio<ExamenMedico>, Repositorio<ExamenMedico>>();
builder.Services.AddScoped<IServicios<ExamenMedico>, Servicios<ExamenMedico>>();
builder.Services.AddScoped<IRepositorio<Prueba>, Repositorio<Prueba>>();
builder.Services.AddScoped<IServicios<Prueba>, Servicios<Prueba>>();

builder.Services.AddScoped<InicializarRoles>();



//Cadena de conexion
builder.Services.AddDbContext<ContextoApp>(Opciones =>
    Opciones.UseSqlServer(builder.Configuration.GetConnectionString("CadenaConexion"))
);


builder.Services.AddAutoMapper(typeof(Program));

//Identity inyectando servicio
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ContextoApp>().AddDefaultTokenProviders();




//url de retorno
builder.Services.ConfigureApplicationCookie(
    opciones => {
        opciones.LoginPath = new PathString("/Home/AccesoInvalido");
        opciones.AccessDeniedPath = new PathString("/Home/RolInvalido");
    });

//COnfiguracion identity

builder.Services.Configure<IdentityOptions>(options => {
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.User.RequireUniqueEmail = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var servicios = scope.ServiceProvider;
    var inicializadorRoles = servicios.GetRequiredService<InicializarRoles>();
    await inicializadorRoles.CrearRoles();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
