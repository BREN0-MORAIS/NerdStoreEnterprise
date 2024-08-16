using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSE.Identidade.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));


builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>() // Suporte as regras de perfil de usuário
    .AddEntityFrameworkStores<ApplicationDbContext>() //(Fonte de dados) contexto que o identity vai utilizar pra ler do banco etc..
    .AddDefaultTokenProviders(); //Tokens padrões do Identity não são os JWT

//adicionado Swagger
builder.Services.AddSwaggerGen(
    c => c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NerdStore Enterprise Identity API",
        Description = "Esta API faz parte do curso ASP.NET Core Enterprise Applications.",
        Contact = new OpenApiContact() { Name = "Breno Morais", Email = "brenomorais@mail.com" },
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licences/MIT") }
    }));



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization(); // Autenticação
app.UseAuthorization(); // Autorização

app.MapControllers();

app.Run();
