using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for transfering operation.</summary>
public record class TransferDto
(
[Required][StringLength(9)] string SenderAccount,
[Required][StringLength(9)] string ReceiverAccount,
[Required] decimal Balance,
[Required] string Currency
);
 