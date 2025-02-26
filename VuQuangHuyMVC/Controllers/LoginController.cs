using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

public class LoginController : Controller
{
    private readonly ISystemAccountService _systemAccountService;
    private readonly IConfiguration _configuration;

    public LoginController(ISystemAccountService service, IConfiguration config)
    {
        _systemAccountService = service;
        _configuration = config;
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>  Login(string username, string password)
    {
        // Lấy tài khoản admin từ appsettings.json
        var adminUsername = _configuration["AdminCredentials:Username"];
        var adminPassword = _configuration["AdminCredentials:Password"];

        if (username == adminUsername && password == adminPassword)
        {
            HttpContext.Session.SetString("Username", username);
            HttpContext.Session.SetString("Role", "Admin"); // Gán quyền Admin
			HttpContext.Session.SetInt32("UserId", 0);

			return RedirectToAction("Index", "Account");
        }

        // Kiểm tra tài khoản trong database
        var user = await _systemAccountService.GetAccountByEmailAndPassword(username, password);

        if (user != null)
        {
            HttpContext.Session.SetString("Username", user.AccountName);

            // Xác định quyền dựa vào RoleId
            if (user.AccountRole == 1)
                HttpContext.Session.SetString("Role", "Staff");
            else if (user.AccountRole == 2)
                HttpContext.Session.SetString("Role", "Lecture");
			HttpContext.Session.SetInt32("UserId", user.AccountId);

			return RedirectToAction("Index", "News");
        }

        ViewBag.Error = "Invalid username or password";
        return View();
    }

	public IActionResult Logout()
	{
		HttpContext.Session.Clear(); // Xóa toàn bộ session
		return RedirectToAction("Login", "Login"); // Chuyển về trang đăng nhập
	}
}
