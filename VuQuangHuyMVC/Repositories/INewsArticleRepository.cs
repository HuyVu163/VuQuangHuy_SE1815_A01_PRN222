using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Repositories
{
	public interface INewsArticleRepository
	{
		Task<IEnumerable<NewsArticle>> GetAll();
		Task<NewsArticle> GetNewsArticleById(int idNews);
        Task UpdateNewsArticle(NewsArticle news);
		Task DeleteNewsArticle(NewsArticle news);
		Task<string> GetNewsArticleIdAsync();
        Task AddNewsArticle(NewsArticle news, int userId);
		Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate);
    }
}
