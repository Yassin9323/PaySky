using System;

namespace BankingSystem.Entities;

public class Account
{
    public int Id { get; set; }

    public required string AccountNumber { get; set; }

    public required string AccountType { get; set; }

    public decimal Balance { get; set;}

    public decimal BalanceWithInterest { get; set; }

    public required DateTime CreatedAt { get; set; }

    public ICollection<Transaction>? Transactions { get; set; }

}
