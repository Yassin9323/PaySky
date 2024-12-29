using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for get the balance of account.</summary>
public record class BalanceDto
(
[Required][StringLength(9)] string AccountNumber,
string AccountType,
[Required] decimal Balance

);
 