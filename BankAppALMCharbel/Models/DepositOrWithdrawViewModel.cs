using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BankRepo;
using BankRepo.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAppALMCharbel.Models
{
    public class DepositOrWithdrawViewModel
    {
        [Display(Name = "Account")]
        [Required(ErrorMessage = "Please specify an account")]
        [Remote(action: "VerifyAccount", controller: "Bank")]
        public int? AccountId { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please specify an amount")]
        [Range(double.Epsilon, double.PositiveInfinity, ErrorMessage = "Please specify an amount greater than 0")]
        [DataType(DataType.Currency, ErrorMessage = "Please specify a valid amount")]
        public decimal Amount { get; set; }

        public SubmitType Submit { get; set; }

        public enum SubmitType
        {
            Deposit,
            Withdraw
        }

    }
}
