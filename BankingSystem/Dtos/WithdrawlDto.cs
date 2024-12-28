using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

public record class WithdrawlDto
(
[Required][StringLength(9)] string AccountNumber,
[Required] decimal Amount
);
 