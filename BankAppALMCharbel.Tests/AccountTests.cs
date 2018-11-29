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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TransferToNull()
        {
            // Act
            account.Transfer(10, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TransferToSelf()
        {
            // Act
            account.Transfer(10, account);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TransferZero()
        {
            // Arrange
            var recipient = new Account();

            // Act
            account.Transfer(0, recipient);
        }

        [TestMethod]
        public void TransferWithVeryEnough()
        {
            // Arrange
            var recipient = new Account();
            account.Balance = 10_000_000;

            // Act
            account.Transfer(10, recipient);

            // Assert
            Assert.AreEqual(9_999_980, account.Balance);
            Assert.AreEqual(10, recipient.Balance);
        }

        [TestMethod]
        public void TransferWithJustEnough()
        {
            // Arrange
            var recipient = new Account();
            account.Balance = 10;

            // Act
            account.Transfer(10, recipient);

            // Assert
            Assert.AreEqual(0, account.Balance);
            Assert.AreEqual(10, recipient.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TransferWithNotEnough()
        {
            // Arrange
            var recipient = new Account();
            account.Balance = 10;

            // Act
            account.Transfer(10_000_000, recipient);
        }
    }
}
