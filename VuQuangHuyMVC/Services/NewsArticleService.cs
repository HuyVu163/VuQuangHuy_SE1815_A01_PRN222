using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Services
{
	public class NewsArticleService : INewsArticleService
	{
		private readonly INewsArticleRepository _repository;

        public NewsArticleService()
        {
            _repository = new NewsArticleRepository();
        }

		public async Task AddNewsArticle(NewsArticle news, int userId)
		{
			await _repository.AddNewsArticle(news, userId);
		}

		public async Task DeleteNewsArticle(NewsArticle news)
		{
			await _repository.DeleteNewsArticle(news);
		}

		public async Task<IEnumerable<NewsArticle>> GetAll()
		{
			var result = await _repository.GetAll();
			Console.WriteLine($"Fetched {result.Count()} articles");
			return result;
		}


		public async Task<NewsArticle> GetNewsArticleById(int idNews)
		{
			return await _repository.GetNewsArticleById(idNews);
		}

        public async Task<string> GetNewsArticleIdAsync()
        {
            return await _repository.GetNewsArticleIdAsync();
        }

        public async Task<List<NewsArticle>> GetReportByDateRange(DateTime startDate, DateTime endDate)
        {
			return await _repository.GetReportByDateRange(startDate, endDate);
        }

        public async Task UpdateNewsArticle(NewsArticle news) => _repository.UpdateNewsArticle(news);
	}
}
