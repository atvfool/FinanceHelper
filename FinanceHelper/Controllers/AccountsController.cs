using Dapper;
using FinanceHelper.Models;
using FinanceHelper.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using FinanceHelper.Utilities;

namespace FinanceHelper.Controllers
{
    /*
     * TODO:
     * Add Validation so that Minimum payment is slightly larger than Interest accrued per period
     * 
     * */
    public class AccountsController : AuthorizedController
    {
        private readonly IDapper _dapper;
        public AccountsController(IDapper dapper)
        {
            _dapper = dapper;
        }
        public IActionResult Index()
        {
            List<AccountsModel> model = new List<AccountsModel>();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserID", User.FindFirst(ClaimTypes.NameIdentifier).Value);

            model = _dapper.GetAll<AccountsModel>("SELECT * FROM Accounts WHERE UserID = @UserID", parameters, CommandType.Text);

            return View(model);
        }
        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            AccountsModel model = new AccountsModel();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("AccountID", id);
            parameters.Add("UserID", User.FindFirst(ClaimTypes.NameIdentifier).Value);

            model = _dapper.Get<AccountsModel>("SELECT * FROM Accounts WHERE AccountID = @AccountID AND UserID = @UserID", parameters, CommandType.Text);

            return View(model);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("AccountName", model.AccountName);
                    parameters.Add("StartingBalance", model.StartingBalance);
                    parameters.Add("InterestRate", model.InterestRate);
                    parameters.Add("MinimumPayment", model.MinimumPayment);
                    parameters.Add("DayOfMonth", model.DayOfMonth);
                    parameters.Add("UserID", User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    string insert = "INSERT INTO Accounts(AccountName, StartingBalance, InterestRate, MinimumPayment, DayOfMonth, UserID) Value(@AccountName, @StartingBalance, @InterestRate, @MinimumPayment, @DayOfMonth, @UserID)";

                    _dapper.Insert<AccountsModel>(insert, parameters, CommandType.Text);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {

            AccountsModel model = new AccountsModel();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("AccountID", id);
            parameters.Add("UserID", User.FindFirst(ClaimTypes.NameIdentifier).Value);

            model = _dapper.Get<AccountsModel>("SELECT * FROM Accounts WHERE AccountID = @AccountID AND UserID = @UserID", parameters, CommandType.Text);

            return View(model);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AccountsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("AccountID", model.AccountID);
                    parameters.Add("AccountName", model.AccountName);
                    parameters.Add("StartingBalance", model.StartingBalance);
                    parameters.Add("InterestRate", model.InterestRate);
                    parameters.Add("MinimumPayment", model.MinimumPayment);
                    parameters.Add("DayOfMonth", model.DayOfMonth);
                    parameters.Add("UserID", User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    string update = @"UPDATE Accounts 
SET AccountName = @AccountName, 
 StartingBalance = @StartingBalance, 
 InterestRate = @InterestRate, 
 MinimumPayment = @MinimumPayment, 
 DayOfMonth = @DayOfMonth
WHERE AccountID = @AccountID  AND UserID = @UserID";

                    _dapper.Update<AccountsModel>(update, parameters, CommandType.Text);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Accounts/Amortization/5
        public ActionResult Amortization(int id)
        {
            AccountsModel account = new AccountsModel();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("AccountID", id);
            parameters.Add("UserID", User.FindFirst(ClaimTypes.NameIdentifier).Value);

            account = _dapper.Get<AccountsModel>("SELECT * FROM Accounts WHERE AccountID = @AccountID AND UserID = @UserID", parameters, CommandType.Text);

            Amortization amortization = new Amortization(account);

            List<AmortizationEntryModel> model = amortization.getAmortization();

            return View(model);
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
