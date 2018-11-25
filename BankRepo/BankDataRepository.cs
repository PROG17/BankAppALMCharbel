using BankRepo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace BankRepo
{
    public class BankDataRepository : BankRepository
    {
        public BankDataRepository()
        {

            var file = new StreamReader("C:\\Users\\charb\\source\\repos\\BankAppALMCharbel\\BankRepo\\bankdata.txt");

            int customers = int.Parse(file.ReadLine());
            for (var i = 0; i < customers; i++)
            {
                _customers.Add(DeserializeCustomer(file.ReadLine()));
            }

            int accounts = int.Parse(file.ReadLine());
            for (var i = 0; i < accounts; i++)
            {
                _accounts.Add(DeserializeAccount(file.ReadLine()));
            }
        }

        internal static Account DeserializeAccount(string line)
        {
            string[] props = line.Split(';');
            return new Account
            {
                Id = int.Parse(props[0], CultureInfo.InvariantCulture),
                CustomerId = int.Parse(props[1], CultureInfo.InvariantCulture),
                Balance = decimal.Parse(props[2], CultureInfo.InvariantCulture)
            };
        }

        internal static Customer DeserializeCustomer(string line)
        {
            string[] props = line.Split(';');
            return new Customer
            {
                Id = int.Parse(props[0], CultureInfo.InvariantCulture),
                OrganizationId = props[1],
                OrganizationName = props[2],
                Address = props[3],
                City = props[4],
                Region = props[5],
                PostCode = props[6],
                Country = props[7],
                Telephone = props[8]
            };
        }
    }
}
