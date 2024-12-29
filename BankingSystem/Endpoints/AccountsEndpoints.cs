using BankingSystem.Dtos;

namespace BankingSystem.Endpoints;

/// <summary>Defines API endpoints for managing accounts and transactions.</summary>
public static class AccountsEndpoints
{

     /// <summary>Maps all account-related endpoints to the web application.</summary>
    /// <param name="app">The web application instance.</param>
    /// <returns>A route group builder with mapped endpoints.</returns>
    public static RouteGroupBuilder MapAccountsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/accounts");

// Get all accounts
        group.MapGet("/", async ( IAccountService accountService) =>
        {
            var accounts = await accountService.GetAllAccountsAsync();
            return Results.Ok(new { Number_Of_Accounts = accounts.Count(), Data = accounts });
        });

// Get account's balance
        group.MapGet("/{id}/balance", async (int id, IAccountService accountService) =>
        {
            var account = await accountService.GetAccountBalanceAsync(id);
            if (account == null) return Results.NotFound("Account not found.");
            return Results.Ok(new { Result = "Successful Operation", Data = account });
        });

// Create account
        group.MapPost("/", async (CreateAccountDto accountDto, IAccountService accountService) =>
        {
            var newAccount = await accountService.CreateAccountAsync(accountDto);
            return Results.Created($"/api/accounts/{newAccount.AccountNumber}",
                     new { Result = "Successful Operation", Data = newAccount });
        });

// Deposit operation
        group.MapPost("/deposit", async (DepositDto depositDto, IAccountService accountService) =>
        {
            var deposit = await accountService.MakeDepositAsync(depositDto);
            return Results.Ok(new { Result = "Successful Operation", Data = deposit });
        });
// Withdrawal operation
        group.MapPost("/withdrawal", async (WithdrawalDto withdrawalDto, IAccountService accountService) =>
        {
            var withdrawal = await accountService.MakeWithdrawalAsync(withdrawalDto);
            return Results.Ok(new { Result = "Successful Operation", Data = withdrawal });
        });
// Transfer operation
        group.MapPost("/transfer", async (TransferDto transferDto, IAccountService accountService) =>
        {
            var transfer = await accountService.TransferAsync(transferDto);
            return Results.Ok(new { Result = "Successful Operation", Data = transfer });
        });

        return group;
    }
}
