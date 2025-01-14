using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for withdrawling operation.</summary>
public record class WithdrawalDto
(
[Required][StringLength(9)] string AccountNumber,
[Required] decimal Balance,
[Required] string Currency
);
 