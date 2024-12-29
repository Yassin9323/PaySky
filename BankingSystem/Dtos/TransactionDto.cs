using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for transactions.</summary>
public record class TransactionDto
(
    string TransactionType,

    string _Amount,

    string Currency,
    
    string TransactionTime
);
