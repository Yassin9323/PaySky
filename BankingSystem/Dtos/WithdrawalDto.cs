using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

public record class WithdrawalDto
(
[Required][StringLength(9)] string AccountNumber,
string AccountType,
[Required] decimal Balance
);
 