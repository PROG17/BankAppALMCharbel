using System;
using BankRepo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankAppALMCharbel.Tests
{
    [TestClass]
    public class AccountTests
    {
        private Account account;

        [TestInitialize]
        public void TestInitialize()
        {
            account = new Account();
        }

        [TestMethod]
        public void DepositValidAmount()
        {
            account.Deposit(10);

            Assert.AreEqual(10, account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DepositInvalidAmount()
        {
            account.Deposit(0);
        }

        [TestMethod]
        public void WithdrawValidAmount()
        {
            account.Balance = 10;

            account.Withdraw(10);

            Assert.AreEqual(0, account.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WithdrawInvalidAmount()
        {
            account.Withdraw(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WithdrawInsufficient()
        {
            account.Withdraw(10);
        }
    }
}
