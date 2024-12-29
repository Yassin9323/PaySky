using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for get all accounts.</summary>
public record class AccountsDto
(
[Required][StringLength(9)] string AccountNumber,
string AccountType,
[Required] decimal Balance

);
 