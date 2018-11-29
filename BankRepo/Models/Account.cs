using System;
using System.Collections.Generic;
using System.Text;

namespace BankRepo.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount < 1) throw new ArgumentException("Amount must be greater than 0!", nameof(amount));

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 1) throw new ArgumentException("Amount must be greater than 0!", nameof(amount));

            if (amount > Balance) throw new InvalidOperationException("Amount can not be greater than account balance!");

            Balance -= amount;
        }

        public void Transfer(decimal amount, Account recipient)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 0!");

            if (recipient is null) throw new ArgumentNullException(nameof(recipient), "Transfer recipient cannot be null.");
            
            if (ReferenceEquals(recipient, this)) throw new ArgumentException("Transfer recipient cannot be same account.", nameof(recipient));

            if (amount > Balance) throw new InvalidOperationException("Amount cannot be greater than account balance!");

            Balance -= amount;
            recipient.Balance += amount;
        }
    }
}
