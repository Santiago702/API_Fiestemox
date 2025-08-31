using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Api_FiesteDocs.Data;
using Api_FiesteDocs.Services.Interfaces;
using Api_FiesteDocs.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);




builder.Configuration.AddJsonFile("appsettings.json");
var secretKey = builder.Configuration.GetSection("settings").GetSection("SecretKey").ToString();
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


// Add services to the container.
builder.Services.AddControllers();
//Servicios
builder.Services.AddScoped<I_Usuario,S_Usuario>();
builder.Services.AddScoped<I_Estudiante,S_Estudiante>();
builder.Services.AddScoped<I_Grupo,S_Grupo>();
builder.Services.AddScoped<I_Seccion,S_Seccion>();
builder.Services.AddScoped<I_Instrumento,S_Instrumento>();
builder.Services.AddScoped<I_Ensayo,S_Ensayo>();
builder.Services.AddScoped<I_Trabajo,S_Trabajo>();
builder.Services.AddScoped<I_Partitura,S_Partitura>();
builder.Services.AddScoped<I_Autenticar, S_Autenticar>();
builder.Services.AddScoped<I_Archivo, S_Archivo>();
builder.Services.AddScoped<I_Dropbox, S_Dropbox>();
// Configuración de Swagger personalizada
builder.Services.AddSwaggerGen(c =>
{
    // Información básica
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API FiesteDocs",
        Version = "v1",
        Description = "API para la gestión de estudiantes y su repertorio en la Fundación de Musicos Fiestemox",
        Contact = new OpenApiContact
        {
            Name = "Santiago Cuervo",
            Email = "santic9999@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/santiago-cuervo-3622ba290/")
        }
    });

    

    // Configuración para usar JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Introduce el token JWT en el formato: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configuración de DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
