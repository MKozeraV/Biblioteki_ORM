using Dapper;
using DapperD.Models;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using DapperD.Models;
using DapperD.Endpoints;
using DapperD.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ApplicationException("Blad w connection stringu");
    return new SqlConnectionFactory(connectionString);  

});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapBooksEndpoints();

app.Run();


