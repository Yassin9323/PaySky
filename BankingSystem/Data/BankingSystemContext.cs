using BankingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Data
{
    /// <summary> Represents the context of the banking system, including the accounts and transactions.</summary>
    public class BankingSystemContext : DbContext
    {
        /// <summary> /// Initializes a new instance of the <see cref="BankingSystemContext"/> class. </summary>
        /// <param name="options">The options to configure the context.</param>
        public BankingSystemContext(DbContextOptions<BankingSystemContext> options)
            : base(options)
        {
        }

        /// <summary> Gets or sets the accounts in the banking system. </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>  Gets or sets the transactions in the banking system. </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary> Configures the model for the banking system, including relationships and constraints. </summary>
        /// <param name="modelBuilder">The model builder used to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountId);
        }


    }
}
