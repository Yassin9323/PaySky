using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

public record class DepositDto
(
[Required][StringLength(9)] string AccountNumber,
[Required] decimal Balance
);
 