using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

public record class TransferDto
(
[Required][StringLength(9)] string SenderAccount,
[Required][StringLength(9)] string ReceiverAccount,
[Required] decimal Balance
);
 