using System;
using BankRepo;
using BankRepo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppALMCharbel.Extensions;
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

        //Testar CI
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
            ModelState.ValidateRemote(nameof(model.AccountTo), ValidateTransferAccountTo(model));
            ModelState.ValidateRemote(nameof(model.AccountFrom), ValidateTransferAccountFrom(model));
            ModelState.ValidateRemote(nameof(model.Amount), ValidateTransferAmount(model));

            if (ModelState.IsValid)
            {
                Account source = _bankRepository.GetAccount(model.AccountFrom);
                Account recipient = _bankRepository.GetAccount(model.AccountTo);

                source.Transfer(model.Amount, recipient);

                ViewData["TransferSuccess"] = true;
            }

            return View(model);
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