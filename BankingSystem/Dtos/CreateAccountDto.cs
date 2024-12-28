using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

public record class CreateAccountDto
(
[Required][StringLength(9)] string AccountNumber,
[Required] decimal Balance,
[Required] string AccountType
);
 