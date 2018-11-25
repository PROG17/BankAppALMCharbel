using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankAppALMCharbel.Models;
using BankRepo;

namespace BankAppALMCharbel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBankRepository _bankRepository;

        public HomeController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public IActionResult Index()
        {
            List<CustomerViewModel> model = _bankRepository.Customers
                .Select(c => new CustomerViewModel
                {
                    Customer = c,
                    Accounts = _bankRepository.GetAccountsFromCustomer(c.Id)
                }).ToList();

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
