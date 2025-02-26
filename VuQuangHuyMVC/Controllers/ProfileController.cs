using Microsoft.AspNetCore.Mvc;
using VuQuangHuyMVC.Models;
using System.Threading.Tasks;
using Services;

public class ProfileController : Controller
{
	private readonly ISystemAccountService _accountService;

	public ProfileController(ISystemAccountService accountService)
	{
		_accountService = accountService;
	}

	public int GetCurrentUserId()
	{
		var userId = HttpContext.Session.GetInt32("UserId");
		return userId ?? 0; // Nếu không có, trả về 0
	}

	// GET: Profile/Edit/{id}
	public async Task<IActionResult> Edit()
	{
		var account = await _accountService.GetAccountById(GetCurrentUserId());
		if (account == null)
		{
			return NotFound();
		}
		return View(account);
	}

	// POST: Profile/Edit/{id}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(SystemAccount model)
	{

		if (ModelState.IsValid)
		{
			await _accountService.UpdateAccount(model);
		}

        return RedirectToAction("Edit", new { id = model.AccountId });
    }
}
