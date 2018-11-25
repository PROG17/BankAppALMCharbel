using BankRepo.Models;
using System.Collections.Generic;

namespace BankRepo
{
    public interface IBankRepository
    {
        IReadOnlyList<Customer> Customers { get; }
        
        IReadOnlyList<Account> Accounts { get; }
    }
}