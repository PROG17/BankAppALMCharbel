using System;
using BankRepo;
using BankRepo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAppALMCharbel.Models;

namespace BankAppALMCharbel.Controllers
{
    public class BankController : Controller
    {
        private readonly IBankRepository _bankRepository;

        public BankController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }


        public IActionResult DepositOrWithdraw(int? id)
        {
            return View(new DepositOrWithdrawViewModel());
        }

        [HttpPost]
        public IActionResult DepositOrWithdraw(DepositOrWithdrawViewModel model)
        {
            if(VerifyAccount(model) is JsonResult json && json.Value is string error)
            {
                ModelState.AddModelError(nameof(model.AccountId), error);
            }

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            Account account = _bankRepository.GetAccount(model.AccountId ?? 0);

            try
            {
                switch (model.Submit)
                {
                    case DepositOrWithdrawViewModel.SubmitType.Deposit:
                        account.Deposit(model.Amount);
                        break;

                    case DepositOrWithdrawViewModel.SubmitType.Withdraw:
                        account.Withdraw(model.Amount);
                        break;
                }

                ViewData["DepositOrWithdrawSucces"] = true;
            }

            catch (Exception e)
            {
                ModelState.AddModelError(nameof(model.Amount), e.Message);
            }

            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyAccount(DepositOrWithdrawViewModel model)
        {
            if(model.AccountId is null)
            {
                return Json($"Please specify an account");
            }
            if(_bankRepository.GetAccount(model.AccountId.Value) is null)
            {
                return Json($"Account #{model.AccountId} does not exist.");
            }

            return Json(true);
        }

        [HttpGet]
        public IActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Transfer(TransferViewModel model)
        {
            return View();
        }

        public IActionResult ValidateTransferAccountFrom(TransferViewModel model)
        {
            Account source = _bankRepository.GetAccount(model.AccountFrom);

            if (source is null)
                return Json($"Account #{model.AccountFrom} does not exist.");

            return Json(true);
        }

        public IActionResult ValidateTransferAccountTo(TransferViewModel model)
        {
            Account target = _bankRepository.GetAccount(model.AccountTo);

            if (target is null)
                return Json($"Account #{model.AccountTo} does not exist.");

            if (model.AccountTo == model.AccountFrom)
                return Json("Account cannot transfer to itself.");

            return Json(true);
        }

        public IActionResult ValidateTransferAmount(TransferViewModel model)
        {
            Account source = _bankRepository.GetAccount(model.AccountFrom);

            if (model.Amount > source?.Balance)
                return Json("Insufficient funds in source account.");

            return Json(true);
        }
    }
}