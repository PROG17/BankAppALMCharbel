using BankRepo.Models;
using System.Collections.Generic;

namespace BankRepo
{
    public interface IBankRepository
    {
        IReadOnlyList<Customer> Customers { get; }
        
        IReadOnlyList<Account> Accounts { get; }

        List<Account> GetAccountsFromCustomer(int customerId);

        Customer GetCustomerFromAccount(int accountId);

        Customer GetCustomer(int customerId);

        Account GetAccount(int accountId);
    }
}