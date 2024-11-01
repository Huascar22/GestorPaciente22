using Controlador;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Modelo.AppContext;
using Modelo.Entidades;
using Modelo.Interfaces;
using Modelo.Repositorio;
using System.Text;
using Vista.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextoApp>(Opciones =>
    Opciones.UseSqlServer(builder.Configuration.GetConnectionString("CadenaConexion"))
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


builder.Services.AddAutoMapper(typeof(Program));

//Identity inyectando servicio
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ContextoApp>().AddDefaultTokenProviders();

builder.Services.AddScoped<IRepositorio<Medico>, Repositorio<Medico>>();
builder.Services.AddScoped<IServicios<Medico>, Servicios<Medico>>();

builder.Services.AddScoped<IRepositorio<Usuario>, Repositorio<Usuario>>();
builder.Services.AddScoped<IServicios<Usuario>, Servicios<Usuario>>();


builder.Services.AddScoped<IRepositorio<Cita>, Repositorio<Cita>>();
builder.Services.AddScoped<IServicios<Cita>, Servicios<Cita>>();
builder.Services.AddScoped<IRepositorio<Paciente>, Repositorio<Paciente>>();
builder.Services.AddScoped<IServicios<Paciente>, Servicios<Paciente>>();
builder.Services.AddScoped<IRepositorio<ExamenMedico>, Repositorio<ExamenMedico>>();
builder.Services.AddScoped<IServicios<ExamenMedico>, Servicios<ExamenMedico>>();
builder.Services.AddScoped<IRepositorio<Prueba>, Repositorio<Prueba>>();
builder.Services.AddScoped<IServicios<Prueba>, Servicios<Prueba>>();
builder.Services.AddScoped<InicializarRoles>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var servicios = scope.ServiceProvider;
    var inicializadorRoles = servicios.GetRequiredService<InicializarRoles>();
    await inicializadorRoles.CrearRoles();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
