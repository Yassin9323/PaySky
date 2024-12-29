using System;

namespace BankingSystem.Entities;

/// <summary>Represents a bank account entity.</summary>
public class Account
{
    /// <summary>Gets or sets the unique identifier of the account.</summary>
    public int Id { get; set; }

    /// <summary>Gets or sets the unique 9-digit account number.</summary>
    public required string AccountNumber { get; set; }

    /// <summary>Gets or sets the type of the account (e.g., SavingsAccount, CheckingAccount).</summary>
    public required string AccountType { get; set; }

    /// <summary>Gets or sets the current balance of the account.</summary>
    public decimal Balance { get; set; }

    /// <summary>Tracks the last date interest was calculated for the account.</summary>
    public DateTime? LastInterestCalculated { get; set; }

    /// <summary>Gets or sets the creation date of the account.</summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>Gets or sets the collection of related transactions.</summary>
    public ICollection<Transaction>? Transactions { get; set; }
}
