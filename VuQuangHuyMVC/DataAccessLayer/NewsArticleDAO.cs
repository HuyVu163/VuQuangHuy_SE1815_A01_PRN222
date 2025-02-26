using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.BusinessObjects;
using VuQuangHuyMVC.Models;

namespace DataAccessLayer
{
	public class NewsArticleDAO
	{
        public static async Task<IEnumerable<NewsArticle>> GetAll()
        {
            try
            {
                using var db = new FunewsManagementContext();
                return await db.NewsArticles.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }


		public static async Task<NewsArticle?> GetNewsArticleById(int idNews)
		{
			try
			{
				await using var db = new FunewsManagementContext();
				var article = await db.NewsArticles.FirstOrDefaultAsync(n => n.NewsArticleId == idNews.ToString());

				if (article == null)
				{
					Console.WriteLine($"No article found with ID {idNews}");
				}

				return article;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in GetNewsArticleById: {ex.Message}");
				return null;
			}
		}

		public static async Task<bool> AddNewsArticle(NewsArticle news, int userId)
		{
			try
			{
				await using var db = new FunewsManagementContext();

				var newArticle = new NewsArticle
				{
					NewsArticleId = news.NewsArticleId,
					NewsTitle = news.NewsTitle,
					Headline = news.Headline,
					CreatedDate = DateTime.Now,  // Tự động set ngày tạo
					NewsContent = news.NewsContent,
					NewsSource = news.NewsSource,
					CategoryId = news.CategoryId,  // Chọn từ dropdown
					NewsStatus = news.NewsStatus,
					CreatedById = (short)userId,  // Người đăng nhập
					UpdatedById = (short)userId,
					ModifiedDate = DateTime.Now
				};

				await db.NewsArticles.AddAsync(newArticle);
				await db.SaveChangesAsync(); // Lưu thay đổi vào DB

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return false;
			}
		}
    
		// Lấy id lớn nhất của news
        public static async Task<string> GetNewsArticleIdAsync()
        {
            await using var db = new FunewsManagementContext();

            // Lấy danh sách ID từ database và chuyển đổi sang số nguyên
            var idList = await db.NewsArticles
                                 .Select(n => n.NewsArticleId)
                                 .ToListAsync();

            // Chuyển đổi danh sách ID từ string -> int, bỏ qua giá trị không hợp lệ
            var validIds = idList.Select(id => int.TryParse(id, out int parsedId) ? parsedId : 0);

            // Lấy ID lớn nhất, cộng 1
            int nextId = validIds.Any() ? validIds.Max() + 1 : 1;

            // Trả về ID mới dưới dạng string
            return nextId.ToString();
        }

        public static async Task UpdateNewsArticle(NewsArticle news)
		{
			try
			{
				using var db = new FunewsManagementContext();
				db.NewsArticles.Update(news);
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public static async Task DeleteNewsArticle(NewsArticle news)
		{
			try
			{
				using var db = new FunewsManagementContext();
				NewsArticle newsExisting = await db.NewsArticles.FirstOrDefaultAsync(n => n.Equals(news));
				if (newsExisting != null)
				{
					db.Remove(newsExisting);
					db.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

        public static async Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate)
        {
            using var db = new FunewsManagementContext();

            return await db.NewsArticles
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

    }
}
