using BankingSystem.Data;
using BankingSystem.Dtos;
using BankingSystem.Entities;
// using BankingSystem.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Endpoints;

public static class AccountsEndpoints
{
    public static RouteGroupBuilder MapAccountsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/accounts");

        // Create new account
        group.MapPost("/", async (CreateAccountDto account, BankingSystemContext dbContext) =>
        {
            var newAccount = new Account
            { 
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                Balance = account.Balance,
                CreatedAt = DateTime.Now
            };

            var accountDto = new CreateAccountDto(
                newAccount.AccountNumber,
                newAccount.Balance,
                newAccount.AccountType
            );

            await dbContext.Accounts.AddAsync(newAccount);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/api/accounts/{account.AccountNumber}", accountDto);
            
        });

        // Get account's balance
        group.MapGet("/{id}/balance", async (int id, BankingSystemContext dbContext) =>
        {
            var account = await dbContext.Accounts.FindAsync(id);
            Console.WriteLine(account);

            if (account == null){
                return Results.NotFound();
            }

            var accountDto = new BalanceDto
            (
                account.AccountNumber,
                account.Balance
            );

            return Results.Ok(accountDto);
        });

        // Make deposit operation
        group.MapPost("/deposit", async (DepositDto deposit, BankingSystemContext dbContext) =>
        {
            // Fetch account by AccountNumber
            var account = await dbContext.Accounts.FirstOrDefaultAsync
                        (a => a.AccountNumber == deposit.AccountNumber);
            if (account == null){
                return Results.NotFound("Account not found.");
            }

            // Validate deposit amount
            if (deposit.Balance <= 0){
                return Results.BadRequest("Deposit amount must be greater than zero.");
            }

            // Update balance
            account.Balance += deposit.Balance;

            // Transaction logging
            var newTransaction = new Transaction()
            {
                AccountId = account.Id,
                TransactionType = "Deposit",
                Amount = deposit.Balance,
                TransactionDate = DateTime.Now,
                Account = account
            };

            // Creating Dto to send it
            var depositDto = new DepositDto
            (
                account.AccountNumber,
                account.Balance
            );

            dbContext.Transactions.Add(newTransaction);
            await dbContext.SaveChangesAsync();

            return Results.Ok(depositDto);
        });



        return group;
    }

}
