using System;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Data;

/// <summary> Provides extension methods for data operations in the banking system. </summary>
public static class DataExtensions
{
    
    /// <summary> Applies database migrations for the given web application.</summary>
    /// <param name="app">The web application to apply migrations to.</param>
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BankingSystemContext>();
        await dbContext.Database.MigrateAsync();
    }
}
