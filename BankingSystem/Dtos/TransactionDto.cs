using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

public record class TransactionDto
(
    [Required] string TransactionType,
    [Required] decimal Amount,
    [Required] DateTime TransactionTime

);
 