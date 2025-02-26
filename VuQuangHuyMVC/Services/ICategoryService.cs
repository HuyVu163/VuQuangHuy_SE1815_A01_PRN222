using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Services
{
	public interface ICategoryService
	{
		Task<IEnumerable<Category>> GetAll();
		Task<Category> GetCategoryById(int idCategory);
		Task AddCategory(Category category);
		Task UpdateCategory(Category category);
		Task DeleteCategory(Category category);
	}
}
