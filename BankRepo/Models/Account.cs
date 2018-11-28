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
    }
}
