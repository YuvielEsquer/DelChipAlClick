using ApiTallerDelChipAlClick.AutoMappers;
using ApiTallerDelChipAlClick.DtoModels;
using ApiTallerDelChipAlClick.Helpers;
using ApiTallerDelChipAlClick.Models;
using ApiTallerDelChipAlClick.Repository;
using ApiTallerDelChipAlClick.Services;
using ApiTallerDelChipAlClick.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// El maldito CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddKeyedScoped<ICommonService<LedsDto, LedsInsertDto, LedsUpdateDto>, LedsService>("LedsService");
builder.Services.AddKeyedScoped<ICommonService<CommonModulesDto, CommonModulesInsertDto, CommonModulesUpdateDto>, CommonModulesService>("CommonModulesService");

// Validadores
builder.Services.AddScoped<IValidator<LedsUpdateDto>, LedsUpdateValidator>();
builder.Services.AddScoped<IValidator<LedsInsertDto>, LedsInsertValidator>();
builder.Services.AddScoped<IValidator<CommonModulesUpdateDto>, CommonModulesUpdateValidator>();
builder.Services.AddScoped<IValidator<CommonModulesInsertDto>, CommonModulesInsertValidator>();

// Repositorio
builder.Services.AddScoped<IRepository<LedsModel>, LedsRepository>();
builder.Services.AddScoped<IRepository<CommonModulesModel>, CommonModulesRepository>();



// Inyección del DbContext
builder.Services.AddDbContext<TallerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TallerDbConnection"));
});

// Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ConfigureHttpsDefaults(httpsOptions =>
    {
        
    });
});

builder.Services.AddSingleton<Utilities>();
builder.Services.AddAuthentication(config => { 
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;//cambiar a true cuando este en produccion 
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["JwT:Audience"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwT:key"]!))

    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilitar CORS
app.UseCors("NewPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
