using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for creating a new account.</summary>
public record class CreateAccountDto
(
    [StringLength(9)] string AccountNumber,

    [Required] string AccountType
);
