using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _repository;

		public CategoryService()
		{
			_repository = new CategoryRepository();
		}

		async public Task AddCategory(Category category)
		{
			await _repository.AddCategory(category);
		}

		async public Task DeleteCategory(Category category)
		{
			await _repository.DeleteCategory(category);
		}

		async public Task<IEnumerable<Category>> GetAll()
		{
			return await _repository.GetAll();
		}

		async public Task<Category> GetCategoryById(int idCategory)
		{
			return await _repository.GetCategoryById(idCategory);
		}

		async public Task UpdateCategory(Category category)
		{
			await _repository.UpdateCategory(category);
		}
	}
}
