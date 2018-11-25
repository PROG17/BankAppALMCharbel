using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankRepo;
using BankRepo.Models;

namespace BankAppALMCharbel.Models
{
    public class CustomerViewModel
    {
        public Customer Customer { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
