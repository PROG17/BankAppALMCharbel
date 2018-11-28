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
    }
}