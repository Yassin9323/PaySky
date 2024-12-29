using AutoMapper;
using BankingSystem.Data;
using BankingSystem.Dtos;
using BankingSystem.Entities;
using Microsoft.EntityFrameworkCore;

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

    public AccountService(BankingSystemContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

//Get all accounts
    public async Task<IEnumerable<AccountsDto>> GetAllAccountsAsync()
    {
        var accounts = await _dbContext.Accounts.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<AccountsDto>>(accounts);
    }

//Get account's balance
    public async Task<BalanceDto?> GetAccountBalanceAsync(int accountId)
    {
        var account = await _dbContext.Accounts.FindAsync(accountId);
        if (account == null) return null;

        return _mapper.Map<BalanceDto>(account);
    }

// Create account
    public async Task<CreateAccountDto> CreateAccountAsync(CreateAccountDto accountDto)
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
        return _mapper.Map<CreateAccountDto>(newAccount);
    }

// Deposit operation
    public async Task<TransactionDto> MakeDepositAsync(DepositDto depositDto)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == depositDto.AccountNumber);
        if (account == null) throw new Exception("Account not found.");
        if (depositDto.Balance <= 0) throw new Exception("Deposit amount must be greater than zero.");

        account.Balance += depositDto.Balance;

        var transaction = new Transaction
        {
            AccountId = account.Id,
            TransactionType = "Deposit",
            Amount = depositDto.Balance,
            TransactionTime = DateTime.Now,
            Account = account
        };

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<TransactionDto>(transaction);
    }

// Withdrawal operation
    public async Task<TransactionDto> MakeWithdrawalAsync(WithdrawalDto withdrawalDto)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == withdrawalDto.AccountNumber);
        if (account == null) throw new Exception("Account not found.");
        if (withdrawalDto.Balance <= 0) throw new Exception("Withdrawal amount must be greater than zero.");

        if (account.AccountType == "CheckingAccount" && withdrawalDto.Balance > account.Balance + 500)
            throw new Exception("Withdrawal exceeds the maximum allowed.");

        if (account.AccountType == "SavingsAccount" && withdrawalDto.Balance > account.Balance)
            throw new Exception("Withdrawal exceeds the maximum allowed.");

        account.Balance -= withdrawalDto.Balance;

        var transaction = new Transaction
        {
            AccountId = account.Id,
            TransactionType = "Withdrawal",
            Amount = withdrawalDto.Balance,
            TransactionTime = DateTime.Now,
            Account = account
        };

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<TransactionDto> TransferAsync(TransferDto transferDto)
    {
        var senderAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferDto.SenderAccount);
        var receiverAccount = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == transferDto.ReceiverAccount);

        if (senderAccount == null || receiverAccount == null) throw new Exception("Account not found.");
        if (transferDto.Balance <= 0) throw new Exception("Transfer amount must be greater than zero.");
        if (senderAccount.Balance < transferDto.Balance) throw new Exception("Insufficient funds.");

        senderAccount.Balance -= transferDto.Balance;
        receiverAccount.Balance += transferDto.Balance;

        var transaction = new Transaction
        {
            AccountId = senderAccount.Id,
            TransactionType = "Transfer",
            Amount = transferDto.Balance,
            TransactionTime = DateTime.Now,
            TransferredTo = receiverAccount.Id,
            Account = senderAccount
        };

        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<TransactionDto>(transaction);
    }
}
