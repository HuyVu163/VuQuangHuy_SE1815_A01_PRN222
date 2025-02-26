using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Services
{
	public interface INewsArticleService
	{
		Task<IEnumerable<NewsArticle>> GetAll();
		Task<NewsArticle> GetNewsArticleById(int idNews);
        Task<string> GetNewsArticleIdAsync();
        Task AddNewsArticle(NewsArticle news, int userId);
        Task UpdateNewsArticle(NewsArticle news);
		Task DeleteNewsArticle(NewsArticle news);
        Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate);
    }
}
