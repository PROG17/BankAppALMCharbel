using BankRepo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankRepo
{
    public class BankRepository : IBankRepository
    {
        protected readonly List<Customer> _customers;
        protected readonly List<Account> _accounts;

        public IReadOnlyList<Customer> Customers => _customers;
        public IReadOnlyList<Account> Accounts => _accounts;

        public BankRepository()
        {
            _customers = new List<Customer>();
            _accounts = new List<Account>();
        }
    }
}
