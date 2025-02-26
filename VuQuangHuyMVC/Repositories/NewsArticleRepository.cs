using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Repositories
{
	public class NewsArticleRepository : INewsArticleRepository
	{
        public async Task<string> GetNewsArticleIdAsync()
        {
            return await NewsArticleDAO.GetNewsArticleIdAsync();
        }

        public async Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate)
        {
            return await NewsArticleDAO.GetReportByDateRange(startDate, endDate);
        }

        async Task INewsArticleRepository.AddNewsArticle(NewsArticle news, int userId)
		{
			await NewsArticleDAO.AddNewsArticle(news, userId);
		}

		async Task INewsArticleRepository.DeleteNewsArticle(NewsArticle news)
		{
			await NewsArticleDAO.DeleteNewsArticle(news);
		}

		async Task<IEnumerable<NewsArticle>> INewsArticleRepository.GetAll()
		{
			return await NewsArticleDAO.GetAll();
		}

		async Task<NewsArticle> INewsArticleRepository.GetNewsArticleById(int idNews)
		{
			return await NewsArticleDAO.GetNewsArticleById(idNews);
		}

		async Task INewsArticleRepository.UpdateNewsArticle(NewsArticle news)
		{
			await NewsArticleDAO.UpdateNewsArticle(news);
		}
	}
}
