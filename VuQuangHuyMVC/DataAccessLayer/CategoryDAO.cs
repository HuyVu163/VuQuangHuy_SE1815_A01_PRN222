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
    public class CategoryDAO
    {
        public static async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                using var db = new FunewsManagementContext();
                return await db.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Category>(); // Return an empty list to prevent null reference issues
            }
        }

        public static async Task<Category> GetCategoryById(int idCategory)
        {
            try
            {
                using var db = new FunewsManagementContext();
                return await db.Categories.FirstOrDefaultAsync(c => c.CategoryId == idCategory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public static async Task AddCategory(Category category)
        {
            try
            {
                using var db = new FunewsManagementContext();
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync(); // Lưu thay đổi vào DB
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static async Task UpdateCategory(Category category)
        {
            try
            {
                using var db = new FunewsManagementContext();
                db.Categories.Update(category);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static async Task DeleteCategory(Category category)
        {
            try
            {
                using var db = new FunewsManagementContext();
                db.Remove(category);
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
