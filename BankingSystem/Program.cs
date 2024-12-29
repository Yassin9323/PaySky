using BankingSystem.Data;
using BankingSystem.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BankingSystem");

// Configure services
builder.Services.AddSqlite<BankingSystemContext>(connString);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

// Pass endpoints directly
app.MapAccountsEndpoints();

await app.MigrateDbAsync();
app.Run();
