using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BankAppALMCharbel.Models
{
    public class TransferViewModel
    {
        [Display(Name = "Source account")]
        [Required(ErrorMessage = "Please specify a source account.")]
        [Remote(controller: "Bank", action: "ValidateTransferAccountFrom")]
        public int AccountFrom { get; set; }

        [Display(Name = "Recipient account")]
        [Required(ErrorMessage = "Please specify a recipient account.")]
        [Remote(controller: "Bank", action: "ValidateTransferAccountTo", AdditionalFields = nameof(AccountFrom))]
        public int AccountTo { get; set; }

        [Required(ErrorMessage = "Please specify an amount.")]
        [DataType(DataType.Currency)]
        [Range(double.Epsilon, double.PositiveInfinity, ErrorMessage = "Please specify a positive amount.")]
        [Remote(controller: "Bank", action: "ValidateTransferAmount", AdditionalFields = nameof(AccountFrom))]
        public decimal Amount { get; set; }
    }
}