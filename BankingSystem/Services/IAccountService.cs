using BankingSystem.Dtos;

public interface IAccountService
{
    Task<IEnumerable<AccountsDto>> GetAllAccountsAsync();
    Task<BalanceDto?> GetAccountBalanceAsync(int accountId);
    Task<CreateAccountDto> CreateAccountAsync(CreateAccountDto accountDto);
    Task<TransactionDto> MakeDepositAsync(DepositDto depositDto);
    Task<TransactionDto> MakeWithdrawalAsync(WithdrawalDto withdrawalDto);
    Task<TransactionDto> TransferAsync(TransferDto transferDto);
}
