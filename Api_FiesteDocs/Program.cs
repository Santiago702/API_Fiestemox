using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Api_FiesteDocs.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configuraci�n de Swagger personalizada
builder.Services.AddSwaggerGen(c =>
{
    // Informaci�n b�sica
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API FiesteDocs",
        Version = "v1",
        Description = "API para la gesti�n de estudiantes y su repertorio en la Fundaci�n de Musicos Fiestemox",
        Contact = new OpenApiContact
        {
            Name = "Santiago Cuervo",
            Email = "santic9999@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/santiago-cuervo-3622ba290/")
        }
    });

    

    // Configuraci�n para usar JWT en Swagger
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

// Configuraci�n de DbContext
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
app.UseAuthorization();
app.MapControllers();
app.Run();
