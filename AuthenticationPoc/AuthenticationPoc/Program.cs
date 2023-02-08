using AuthenticationPoc.Helpers;
using AuthenticationPoc.Interfaces.Helpers;
using AuthenticationPoc.Interfaces.Repositories;
using AuthenticationPoc.Interfaces.Workers;
using AuthenticationPoc.Repositories;
using AuthenticationPoc.Validators;
using AuthenticationPoc.Workers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Validators
builder.Services.AddSingleton<LoginDtoValidator>();
builder.Services.AddSingleton<RegisterDtoValidator>();

//Helpers
builder.Services.AddSingleton<IJwtTokenManager, JwtTokenManager>();

//WorkerServices:
builder.Services.AddSingleton<IAuthenticationWorker, AuthenticationWorker>();

//RepositoryServices:
builder.Services.AddSingleton<IUserRepository, UserRepository>();

//ForeignerServices
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("JwtSettings:SecurityKey").Value)),
            ValidateIssuer = false, 
            ValidateAudience = false

        }
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
        options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standart Authorization header  using the Bearer Scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        }
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
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
