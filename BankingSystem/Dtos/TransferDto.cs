using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

public record class TransferDto
(
[Required][StringLength(9)] string AccountNumber,
[Required][StringLength(9)] string TransferToAccount,
[Required] decimal Amount
);
 