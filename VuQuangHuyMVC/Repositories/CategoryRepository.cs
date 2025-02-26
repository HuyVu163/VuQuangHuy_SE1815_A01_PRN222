using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		async Task ICategoryRepository.AddCategory(Category category)
		{
			await CategoryDAO.AddCategory(category);
		}

		async Task ICategoryRepository.DeleteCategory(Category category)
		{
			await CategoryDAO.DeleteCategory(category);
		}

		async Task<IEnumerable<Category>> ICategoryRepository.GetAll()
		{
			return await CategoryDAO.GetAll();
		}

		async Task<Category> ICategoryRepository.GetCategoryById(int idCategory)
		{
			return await CategoryDAO.GetCategoryById(idCategory);
		}

		async Task ICategoryRepository.UpdateCategory(Category category)
		{
			await CategoryDAO.UpdateCategory(category);
		}
	}
}
