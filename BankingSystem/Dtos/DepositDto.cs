using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for depositing operation.</summary>
public record class DepositDto
(
    
[Required][StringLength(9)] string AccountNumber,

[Required] decimal Balance,

[Required] string Currency

);
 