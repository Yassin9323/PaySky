using BankingSystem.Dtos;

/// <summary>Interface for separation the bussiness logic and endpoints for several reasons.</summary>
public interface IAccountService
{
    /// <summary>Retrieves all accounts in the system.</summary>
    Task<IEnumerable<AccountsDto>> GetAllAccountsAsync();

    /// <summary>Retrieves the balance of a specific account.</summary>
    /// <param name="accountId">The ID of the account.</param>
    Task<BalanceDto?> GetAccountBalanceAsync(int accountId);

    /// <summary>Creates a new account with the provided details.</summary>
    /// <param name="accountDto">The account details to create.</param>
    Task<CreateAccountDto> CreateAccountAsync(CreateAccountDto accountDto);

    /// <summary>Deposits money into an account.</summary>
    /// <param name="depositDto">The deposit details.</param>
    Task<TransactionDto> MakeDepositAsync(DepositDto depositDto);

    /// <summary>Withdraws money from an account.</summary>
    /// <param name="withdrawalDto">The withdrawal details.</param>
    Task<TransactionDto> MakeWithdrawalAsync(WithdrawalDto withdrawalDto);

    /// <summary>Transfers funds between two accounts.</summary>
    /// <param name="transferDto">The transfer details.</param>
    Task<TransactionDto> TransferAsync(TransferDto transferDto);
}
