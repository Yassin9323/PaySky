using BankingSystem.Data;
using BankingSystem.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("BankingSystem");

// Configure services
builder.Services.AddSqlite<BankingSystemContext>(connString);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IAccountService, AccountService>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Banking System API",
        Version = "v1",
        Description = "API documentation for the Banking System project."
    });

    // Include XML comments if generated
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Enable Swagger middleware for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Generate Swagger JSON
    app.UseSwaggerUI(); // Serve Swagger UI at /swagger
}

// Pass endpoints directly
app.MapAccountsEndpoints();

await app.MigrateDbAsync();
app.Run();
