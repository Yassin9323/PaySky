using BankingSystem.Data;
using BankingSystem.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BankingSystem");

builder.Services.AddSqlite<BankingSystemContext>(connString);

var app = builder.Build();

app.MapAccountsEndpoints();

await app.MigrateDbAsync();

app.Run();
