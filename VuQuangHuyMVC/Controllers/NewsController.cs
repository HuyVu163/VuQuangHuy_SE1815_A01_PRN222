using BusinessLogicLayer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Security.Claims;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace VuQuangHuyMVC.Controllers
{
	public class NewsController : Controller
	{

		private readonly NewsArticleService newsService;
		private readonly CategoryService categoryService;
		private readonly SystemAccountService systemAccountService;
		private readonly EmailService emailService;

		public NewsController(EmailService emailService)
		{
			newsService = new NewsArticleService();
			categoryService = new CategoryService();
			systemAccountService = new SystemAccountService();
			this.emailService = emailService;
		}

		public int GetCurrentUserId()
		{
			var userId = HttpContext.Session.GetInt32("UserId");
			return userId ?? 0; // Nếu không có, trả về 0
		}
		public string GetCurrentUserRole()
		{
			var userRole = HttpContext.Session.GetString("Role");
			return userRole ?? "User"; // Nếu không có, trả về 0
		}


		// GET: NewsController
		public async Task<IActionResult> Index()
		{
			// Lấy ID lớn nhất trong bảng NewsArticle và +1
			string nextNewsArticleId = await newsService.GetNewsArticleIdAsync();

			// Lấy danh sách Categories để hiển thị trong dropdown
			ViewBag.Categories = new SelectList(await categoryService.GetAll(), "CategoryId", "CategoryName");

			// Lấy ID của user đang đăng nhập
			ViewBag.CurrentUserId = GetCurrentUserId(); // Hàm lấy UserID từ session hoặc token

			// Gửi ID mới sang View
			ViewBag.NextNewsArticleId = nextNewsArticleId;

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
				news = (await newsService.GetAll()).ToList();
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



		// GET: NewsController/Details/5
		public async Task<IActionResult> Details(int id)
		{
			var newsArticle = await newsService.GetNewsArticleById(id);
			if (newsArticle == null)
			{
				return NotFound();
			}

			var accounts = await systemAccountService.GetAll();
			var categories = await categoryService.GetAll();

			// Kiểm tra giá trị của CreatedById và UpdatedById
			var createdBy = accounts.FirstOrDefault(a => a.AccountId == newsArticle.CreatedById);
			var updatedBy = accounts.FirstOrDefault(a => a.AccountId == newsArticle.UpdatedById);
			var category = categories.FirstOrDefault(c => c.CategoryId == newsArticle.CategoryId);

			ViewBag.CreatedByName = createdBy != null ? createdBy.AccountName : "Unknown";
			ViewBag.UpdatedByName = updatedBy != null ? updatedBy.AccountName : "Unknown";
			ViewBag.CategoryName = category != null ? category.CategoryName : "Unknown";
			ViewBag.NewsStatus = newsArticle.NewsStatus == true ? "Active" : "Inactive";

			// Kiểm tra dữ liệu trong console log
			Console.WriteLine($"CreatedBy: {ViewBag.CreatedByName}, UpdatedBy: {ViewBag.UpdatedByName}, Category: {ViewBag.CategoryName}");

			return View(newsArticle);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] NewsArticle newsArticle)
		{
			Console.WriteLine($"Title: {newsArticle.NewsTitle}, Content: {newsArticle.NewsContent}, CategoryId: {newsArticle.CategoryId}");

			if (newsArticle == null)
			{
				return BadRequest("Invalid data received.");
			}

			if (ModelState.IsValid)
			{
				string adminEmail = "huyvqhe170932@fpt.edu.vn";
				string subject = "New Article Published: " + newsArticle.NewsTitle;
				string body = $@"
                <h2>New Article Published</h2>
                <p><strong>Title:</strong> {newsArticle.NewsTitle}</p>
                <p><strong>Content:</strong> {newsArticle.NewsContent}</p>
                <p><a href='https://localhost:7127/News/Details?id={newsArticle.NewsArticleId}'>View Article</a></p>";

				await emailService.SendEmailAsync(adminEmail, subject, body);
				await newsService.AddNewsArticle(newsArticle, GetCurrentUserId());
				return Json(new { success = true });
			}

			return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
		}

		[HttpGet]
		public async Task<IActionResult> GetNewsById(int id)
		{
			var news = await newsService.GetNewsArticleById(id);
			if (news == null)
			{
				return NotFound();
			}
			return Json(new
			{
				newsArticleId = news.NewsArticleId,
				newsTitle = news.NewsTitle,
				headline = news.Headline,
				newsContent = news.NewsContent,
				newsSource = news.NewsSource,
				categoryId = news.CategoryId,
				newsStatus = news.NewsStatus
			});
		}

		[HttpPost]
		public async Task<IActionResult> UpdateNews(NewsArticle news)
		{
			var existingNews = await newsService.GetNewsArticleById(int.Parse(news.NewsArticleId));
			if (existingNews == null)
			{
				return Json(new { success = false, error = "News article not found!" });
			}

			existingNews.NewsArticleId = news.NewsArticleId;
			existingNews.NewsTitle = news.NewsTitle;
			existingNews.Headline = news.Headline;
			existingNews.CreatedDate = DateTime.Now;
			existingNews.NewsContent = news.NewsContent;
			existingNews.NewsSource = news.NewsSource;
			existingNews.CategoryId = news.CategoryId;
			existingNews.NewsStatus = news.NewsStatus;
			existingNews.CreatedById = news.CreatedById;
			existingNews.UpdatedById = (short)GetCurrentUserId();
			existingNews.ModifiedDate = DateTime.Now;

			await newsService.UpdateNewsArticle(existingNews);

			return Json(new { success = true });
		}



		// GET: NewsController/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			var newsArticle = await newsService.GetNewsArticleById(id);
			if (newsArticle == null)
			{
				return NotFound();
			}

			await newsService.DeleteNewsArticle(newsArticle);
			return RedirectToAction(nameof(Index));
		}

		// POST: NewsController/Delete/5
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

		[HttpGet]
		public async Task<IActionResult> Search(string title)
		{
			var userRole = GetCurrentUserRole(); // Hàm lấy role người dùng hiện tại

			var newsData = await newsService.GetAll();

			var newsList = newsData
				.Where(n => n.NewsTitle.Contains(title, StringComparison.OrdinalIgnoreCase))
				.Select(n => new
				{
					n.NewsArticleId,
					n.NewsTitle,
					n.Headline,
					CreatedDate = n.CreatedDate?.ToString("dd/MM/yyyy") ?? "N/A",
					n.NewsSource,

					// Nếu role là Staff, hiển thị đầy đủ các cột
					NewsStatus = userRole == "Staff" ? (n.NewsStatus == true ? "Active" : "Inactive") : null,
					UpdatedById = userRole == "Staff" ? n.UpdatedById : null,
					ModifiedDate = userRole == "Staff" ? (n.ModifiedDate?.ToString("dd/MM/yyyy") ?? "N/A") : null
				})
				.ToList();

			return Json(newsList);
		}

		public IActionResult Reports()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Reports(DateTime startDate, DateTime endDate)
		{
			var reportData = await newsService.GetReportByDateRange(startDate, endDate);
			return View(reportData);
		}
	}
}
