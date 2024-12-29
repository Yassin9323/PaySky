using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for transactions.</summary>
public record class TransactionDto
(
    [Required] string TransactionType,
    [Required] decimal amount,
    [Required] string Amount,
    [Required] string TransactionTime

);
