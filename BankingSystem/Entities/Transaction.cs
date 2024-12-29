using System;

namespace BankingSystem.Entities;

/// <summary>Represents a transaction associated with a bank account.</summary>
public class Transaction
{
    /// <summary>Gets or sets the unique identifier of the transaction.</summary>
    public int Id { get; set; }

    /// <summary>Gets or sets the ID of the associated account.</summary>
    public int AccountId { get; set; }

    /// <summary>Gets or sets the type of the transaction (e.g., Deposit, Withdrawal, Transfer).</summary>
    public required string TransactionType { get; set; }

    /// <summary>Gets or sets the transaction amount.</summary>
    public decimal Amount { get; set; }

    /// <summary>Gets or sets the amount's currency.</summary>
    public string Currency { get; set; }

    /// <summary>Gets or sets the timestamp of the transaction.</summary>
    public DateTime TransactionTime { get; set; }

    /// <summary>Gets or sets the ID of the account this transaction was transferred to, if applicable.</summary>
    public int? TransferredTo { get; set; }

    /// <summary>Gets or sets the related account for this transaction.</summary>
    public required Account Account { get; set; }
}
