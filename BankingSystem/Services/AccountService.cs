using AutoMapper;
using BankingSystem.Data;
using BankingSystem.Dtos;
using BankingSystem.Entities;
using Microsoft.EntityFrameworkCore;

/// <summary> Using Interface to handel accounts operation </summary>
public class AccountService : IAccountService
{
    private readonly BankingSystemContext _dbContext;
    private readonly IMapper _mapper;

    // Generate a random 9-digit account number
    private static string GenerateRandomAccountNumber()
    {
        var random = new Random();
        return random.Next(100000000, 999999999).ToString(); // Generates a 9-digit number
    }

    private static decimal convertCurrency(decimal withdrawalBalance, string currency)
    {
        decimal balanceInDollar;

        if (currency == "GBP"){
            balanceInDollar = withdrawalBalance * 1.26m;

        } else if (currency == "EUR"){
            balanceInDollar = withdrawalBalance * 1.04m;

        } else if (currency == "CHF"){
            balanceInDollar = withdrawalBalance * 1.11m;

        } else if (currency == "JPY"){
            balanceInDollar = withdrawalBalance * 0.0063m;

        } else if (currency == "AED" || currency == "SAR"){
        balanceInDollar = withdrawalBalance * 0.27m;

        } else if (currency == "JOD"){
        balanceInDollar = withdrawalBalance * 1.41m;
        
        } else if (currency == "CAD"){
        balanceInDollar = withdrawalBalance * 0.69m;
            
        }else{ balanceInDollar = withdrawalBalance;}

    return balanceInDollar;
    }


    /// <summary> For using DB and AutoMapper </summary>
    public AccountService(BankingSystemContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

/// <summary>Retrieves all accounts in the system.</summary>
    public async Task<IEnumerable<AccountsDto>> GetAllAccountsAsync()
    {
        var accounts = await _dbContext.Accounts.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<AccountsDto>>(accounts);
    }

/// <summary>Gets the balance of a specific account.</summary>
    public async Task<AccountsDto?> GetAccountBalanceAsync(int accountId)
    {
        var account = await _dbContext.Accounts.FindAsync(accountId);
        if (account == null) return null;

        return _mapper.Map<AccountsDto>(account);
    }

/// <summary>Creates a new account with a unique 9-digit number.</summary>
    public async Task<AccountsDto> CreateAccountAsync(CreateAccountDto accountDto)
    {
        // Generate a unique 9-digit account number
        string accountNumber;
        do
        {
            accountNumber = GenerateRandomAccountNumber();
        }
        while (await _dbContext.Accounts.AnyAsync(a => a.AccountNumber == accountNumber)); // Ensure uniqueness

        // Map DTO to entity and assign generated account number and created time
        var newAccount = _mapper.Map<Account>(accountDto);
        newAccount.AccountNumber = accountNumber;
        newAccount.CreatedAt = DateTime.Now;

        // Save the new account to the database
        await _dbContext.Accounts.AddAsync(newAccount);
        await _dbContext.SaveChangesAsync();

        // Map back to DTO and return
        return _mapper.Map<AccountsDto>(newAccount);
    }

/// <summary>Makes a deposit into an account.</summary>
    public async Task<TransactionDto> MakeDepositAsync(DepositDto depositDto)
    {


        // Get the Customer's account by his account number
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == depositDto.AccountNumber);

        // Checking accounts and balance 
        if (account == null) throw new Exception("Account not found.");
        if (depositDto.Balance <= 0) throw new Exception("Deposit amount must be greater than zero.");
        if (depositDto.Currency == null) throw new Exception("Please add the currency of your amount USD - EUR - GBP ");
        
        // Converting Currencies to Dollar $
        decimal balance = convertCurrency(depositDto.Balance, depositDto.Currency);

        // Fixed annual interest rate
        const decimal fixedInterestRate = 0.18m; // 18%

        // Calculate interest if the account is a SavingsAccount
        if (account.AccountType == "SavingsAccount")
        {
            var now = DateTime.Now;
            if (account.LastInterestCalculated.HasValue)
            {
                // Calculate elapsed time in years since the last interest calculation
                var elapsedTime = (now - account.LastInterestCalculated.Value).TotalDays / 365;
                if (elapsedTime > 0)
                {
                    // Apply interest to the balance using the fixed rate
                    var interest = account.Balance * fixedInterestRate * (decimal)elapsedTime;
                    account.Balance += interest;
                }
            }

            // Update LastInterestCalculated to now
            account.LastInterestCalculated = now;
        }
        // Balance operation
        account.Balance += balance;
        
        // Create transaction, store it , and update DB
        var transaction = new Transaction
        {
            AccountId = account.Id,
            TransactionType = "Deposit",
            Amount = depositDto.Balance,
            Currency = depositDto.Currency,
            TransactionTime = DateTime.Now,
            Account = account
        };

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();

        // return Dto to send it to the customer
        return  _mapper.Map<TransactionDto>(transaction);
    }

/// <summary>Makes a withdrawal from an account.</summary>
    public async Task<TransactionDto> MakeWithdrawalAsync(WithdrawalDto withdrawalDto)
    {
        // Get the Customer's account by his account number
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == withdrawalDto.AccountNumber);

        // Checking accounts and balance 
        if (account == null) throw new Exception("Account not found.");
        if (withdrawalDto.Balance <= 0) throw new Exception("Withdrawal amount must be greater than zero.");
        if (withdrawalDto.Currency == null) throw new Exception("Please add the currency of your amount USD - EUR - GBP ");

        // Converting Currencies to Dollar $
        decimal balance = convertCurrency(withdrawalDto.Balance, withdrawalDto.Currency);

        if (account.AccountType == "CheckingAccount" && balance > account.Balance + 500)
            throw new Exception("Withdrawal exceeds the maximum allowed.");

        if (account.AccountType == "SavingsAccount" && balance > account.Balance)
            throw new Exception("Withdrawal exceeds the maximum allowed.");
        
        // Balance operation
        account.Balance -= balance;

        // Create transaction, store it , and update DB
        var transaction = new Transaction
        {
            AccountId = account.Id,
            TransactionType = "Withdrawal",
            Amount = withdrawalDto.Balance,
            Currency = withdrawalDto.Currency,
            TransactionTime = DateTime.Now,
            Account = account
        };

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();

        // return Dto to send it to the customer
        return _mapper.Map<TransactionDto>(transaction);
    }

/// <summary>Transfers funds between two accounts.</summary>
    public async Task<TransactionDto> TransferAsync(TransferDto transferDto)
    {
        // Get the 2 accounts by the account number
        var senderAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferDto.SenderAccount);
        var receiverAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferDto.ReceiverAccount);
        if (transferDto.Currency == null) throw new Exception("Please add the currency of your amount USD - EUR - GBP ");

        // Converting Currencies to Dollar $
        decimal balance = convertCurrency(transferDto.Balance, transferDto.Currency);

        // Checking accounts and balance 
        if (senderAccount == null || receiverAccount == null) throw new Exception("Account not found.");
        if (balance <= 0) throw new Exception("Transfer amount must be greater than zero.");
        if (senderAccount.Balance < balance) throw new Exception("Insufficient funds.");

        // Balance operation
        senderAccount.Balance -= balance;
        receiverAccount.Balance += balance;

        // Create transaction, store it , and update DB
        var transaction = new Transaction
        {
            AccountId = senderAccount.Id,
            TransactionType = "Transfer",
            Amount = transferDto.Balance,
            Currency = transferDto.Currency,
            TransactionTime = DateTime.Now,
            TransferredTo = receiverAccount.Id,
            Account = senderAccount
        };

        
        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();

        // return Dto to send it to the customer
        return _mapper.Map<TransactionDto>(transaction);
    }
}
