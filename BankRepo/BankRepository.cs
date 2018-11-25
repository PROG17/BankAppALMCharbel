using BankRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Account> GetAccountsFromCustomer(int customerId)
        {
            Customer customer = GetCustomer(customerId);
            if (customer is null) return new List<Account>();

            return Accounts.Where(a => a.CustomerId == customerId).ToList();
        }

        public Customer GetCustomerFromAccount(int accountId)
        {
            Account account = GetAccount(accountId);
            if (account is null) return null;

            return GetCustomer(account.CustomerId);
        }

        public Customer GetCustomer(int customerId)
        {
            return _customers.SingleOrDefault(c => c.Id == customerId);
        }

        public Account GetAccount(int accountId)
        {
            return _accounts.SingleOrDefault(a => a.Id == accountId);
        }
    }
}
