using AuthenticationPoc.Helpers;
using AuthenticationPoc.Interfaces.Helpers;
using AuthenticationPoc.Interfaces.Workers;
using AuthenticationPoc.Validators;
using AuthenticationPoc.Workers;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Validators
builder.Services.AddSingleton<UserDtoValidator>();

//Helpers
builder.Services.AddSingleton<IJwtTokenManager, JwtTokenManager>();

//WorkerServices:
builder.Services.AddSingleton<IAuthenticationWorker, AuthenticationWorker>();

//RepositoryServices:

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
