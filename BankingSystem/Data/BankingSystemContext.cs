using BankingSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Data
{
    public class BankingSystemContext : DbContext
    {
        public BankingSystemContext(DbContextOptions<BankingSystemContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountId);
        }


    }
}
