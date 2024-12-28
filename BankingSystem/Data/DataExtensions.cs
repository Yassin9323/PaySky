using System;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BankingSystemContext>();
        await dbContext.Database.MigrateAsync();
    }
}
