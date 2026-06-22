using Bitirme.Business.Concrete;
using Bitirme.Repository.Abstract;
using Bitirme.Repository.Concrete;
using Bitirme.Repository.Data;
using Bitirme.Services;
using Bitirme.Services.Abstract;
using Bitirme.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Business katmanýndaki servislerin sisteme tanýtýlmasý
builder.Services.AddScoped<IAiService, AiManager>();
builder.Services.AddScoped<ITestService, TestManager>();
builder.Services.AddScoped<IAuthService, AuthManager>();
// Repository'leri sisteme tanýtýyoruz
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });


    // 1. JWT Kimlik Doðrulama Servislerini Ekle
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    var app = builder.Build();

    app.UseCors("AllowAll");


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
