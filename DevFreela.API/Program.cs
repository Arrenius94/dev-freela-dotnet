using System.Reflection;
using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using DevFreela.API.Filters;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidationFilter));
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidation>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
/*builder.Services.AddOpenApi();*/

/*builder.Services.Configure<OpenTimeOption>(builder.Configuration.GetSection("OpenTime"));*/

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");
builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));

// Injeção de dependência, para usar a classe ProjectService, que é a implementação da interface IProjectService, que é a interface que define os
// métodos que a classe ProjectService deve implementar, então a gente registra a interface e a implementação, e quando a gente for
// usar a interface, o ASP.NET Core vai saber que tem que usar a implementação, e vai criar uma instância da classe ProjectService e injetar onde for necessário,
// e o AddScoped significa que a instância da classe ProjectService vai ser criada para cada requisição, ou seja, se tiver 10 requisições ao mesmo tempo, vai ser criada
// 10 instâncias da classe ProjectService, uma para cada requisição, e quando a requisição terminar, a instância da classe ProjectService vai ser descartada, ou seja, ela vai ser destruída e liberada da memória,
// então o AddScoped é um ciclo de vida de serviço que é recomendado para serviços que precisam de uma instância por requisição, como é o caso do ProjectService, que precisa de uma instância por requisição para acessar
// o banco de dados e realizar as operações necessárias.
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
/*builder.Services.AddScoped<ExampleClass>(e => new ExampleClass { Name = "Initial Stage" })*/
;

builder.Services.AddEndpointsApiExplorer();

// p usar CQRS, tem que registrar os handlers, e o MediatR tem um método para registrar todos os handlers de uma assembly, então a gente passa a assembly onde estão os handlers,
// que é a mesma onde estão os comandos, então a gente pega a assembly do CreateProjectCommand, que é o comando mais simples que temos,
// e registra os handlers dessa assembly
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly); 
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta maneira: Bearer {seu_token}"
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                
                return context.Response.WriteAsJsonAsync(new { 
                    message = "Você precisa estar logado para acessar este recurso." 
                });
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                
                return context.Response.WriteAsJsonAsync(new { 
                    message = "Seu perfil de usuário não tem permissão para realizar esta ação." 
                });
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();;

app.MapControllers();

app.Run();