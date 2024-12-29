using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Dtos;

/// <summary>Data transfer object for get all accounts.</summary>
public record class AccountsDto
(
[StringLength(9)] string AccountNumber,
string AccountType,
string _Balance

);
 