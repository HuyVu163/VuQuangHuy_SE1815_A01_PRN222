using Azure;
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
	public class TagDAO
	{
		public static async Task<IEnumerable<Tag>> GetAll()
		{
			try
			{
				using var db = new FunewsManagementContext();
				return await db.Tags.ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return new List<Tag>(); // Return an empty list to prevent null reference issues
			}
		}

		public static async Task<Tag> GetTagById(int idTag)
		{
			try
			{
				using var db = new FunewsManagementContext();
				return await db.Tags.FirstOrDefaultAsync(t => t.TagId == idTag);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return null;
			}
		}

		public static async Task AddTag(Tag tag)
		{
			try
			{
				using var db = new FunewsManagementContext();
				await db.Tags.AddAsync(tag);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public static async Task UpdateTag(Tag tag)
		{
			try
			{
				using var db = new FunewsManagementContext();
				db.Tags.Update(tag);
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public static async Task DeleteTag(Tag tag)
		{
			try
			{
				using var db = new FunewsManagementContext();
				db.Tags.Remove(tag);
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

	}
}
