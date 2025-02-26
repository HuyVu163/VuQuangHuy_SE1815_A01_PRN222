using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Services;
using VuQuangHuyMVC.Models;

namespace VuQuangHuyMVC.Controllers
{
	public class HistoryController : Controller
	{
		private readonly NewsArticleService newsService;
		private readonly SystemAccountService systemAccountService;

		public HistoryController()
		{
			newsService = new NewsArticleService();
			systemAccountService = new SystemAccountService();
		}

		public int GetCurrentUserId()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			return userId ?? 0; // Nếu không có, trả về 0
		}


		// GET: NewsController
		public async Task<IActionResult> Index()
		{
			// Lấy danh sách tài khoản từ service
			var accounts = await systemAccountService.GetAll();

			// Kiểm tra xem danh sách accounts có dữ liệu không
			if (accounts == null || !accounts.Any())
			{
				Console.WriteLine("⚠ Không có tài khoản nào trong database!");
			}

			// Lưu danh sách accounts vào ViewBag
			ViewBag.Accounts = accounts;

			// Lấy role của user đang đăng nhập từ session
			var role = HttpContext.Session.GetString("Role");

			List<NewsArticle> news;

			if (role == "Staff")
			{
				// Nếu là Staff, hiển thị tất cả bài viết
				news = (await newsService.GetAll()).Where(n => n.CreatedById == GetCurrentUserId()).ToList();


			}
			else
			{
				// Nếu không có role hoặc role null, chỉ hiển thị bài viết có Status = true
				news = (await newsService.GetAll()).Where(n => n.NewsStatus == true).ToList();
			}


			// Kiểm tra dữ liệu của news
			if (news == null || !news.Any())
			{
				Console.WriteLine("⚠ Không có bài viết nào trong database!");
			}

			return View(news);
		}

		// GET: HistoryController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: HistoryController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: HistoryController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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

		// GET: HistoryController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: HistoryController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
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

		// GET: HistoryController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: HistoryController/Delete/5
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
