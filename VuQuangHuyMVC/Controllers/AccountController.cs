using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using VuQuangHuyMVC.Models;

namespace VuQuangHuyMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SystemAccountService systemAccountService;

        public AccountController()
        {
            systemAccountService = new SystemAccountService();
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            string nextAccountId = await systemAccountService.GetNextAccountIdAsync();
            ViewBag.NextAccountId = nextAccountId;

            var account = await systemAccountService.GetAll();
            return View(account);
        }



        [HttpPost]
        public async Task<IActionResult> Create(SystemAccount model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Invalid model data." });
            }

            try
            {
                // Kiểm tra xem email đã tồn tại hay chưa
                var existingAccount = await systemAccountService.GetAccountByEmail(model.AccountEmail);
                if (existingAccount != null)
                {
                    return Json(new { success = false, message = "Account with this email already exists." });
                }

                // Thêm tài khoản vào database
                await systemAccountService.AddAccount(model);
                return Json(new { success = true, message = "Account created successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var account = await systemAccountService.GetAccountById(id);
            if (account == null)
            {
                return Json(new { success = false, message = "Account not found." });
            }

            return Json(new
            {
                success = true,
                data = new
                {
                    account.AccountId,
                    account.AccountName,
                    account.AccountEmail,
                    account.AccountRole,
                    account.AccountPassword
                }
            });
        }


        // POST: Account/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(SystemAccount model)
        {
            if (ModelState.IsValid)
            {
                var existingAccount = await systemAccountService.GetAccountById(model.AccountId);
                if (existingAccount == null)
                {
                    return Json(new { success = false, message = "Account not found." });
                }

                existingAccount.AccountName = model.AccountName;
                existingAccount.AccountEmail = model.AccountEmail;
                existingAccount.AccountRole = model.AccountRole;
                existingAccount.AccountPassword = model.AccountPassword;

                await systemAccountService.UpdateAccount(existingAccount);
                return Json(new { success = true, message = "Account updated successfully!" });
            }

            return Json(new { success = false, message = "Invalid model data." });
        }


        
        public async Task<IActionResult> Details(int id)
        {
            var account = await systemAccountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound(); // Tránh truyền null vào View
            }
            return View(account);
        }


        // GET: Account/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await systemAccountService.GetAccountById(id); // Tìm tài khoản theo id
            if (account == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy tài khoản
            }

            await systemAccountService.DeleteAccount(account); // Xóa tài khoản

            return RedirectToAction("Index"); // Chuyển về trang danh sách tài khoản
        }



    }
}
