using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

public record class CreateAccountDto
(
[StringLength(9)] string AccountNumber,
[Required] string AccountType,
decimal Balance = 0
);
