using System;

namespace BankingSystem.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public required string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionTime { get; set; }
    public int? TransferredTo { get; set; }

    // Navigation property for the related account
    public required Account Account { get; set; }
}
