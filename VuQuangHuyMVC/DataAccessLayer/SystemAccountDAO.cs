using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.BusinessObjects;
using VuQuangHuyMVC.Models;

namespace DataAccessLayer
{
	public class SystemAccountDAO
	{
		public static async Task<IEnumerable<SystemAccount>> GetAll()
		{
			try
			{
				using var db = new FunewsManagementContext();
				return await db.SystemAccounts.ToListAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return new List<SystemAccount>(); // Return an empty list to prevent null reference issues
			}
		}

		public static async Task<SystemAccount> GetAccountByEmail (string email)
		{
            try
            {
                using var db = new FunewsManagementContext();
                return await db.SystemAccounts.FirstOrDefaultAsync(a => a.AccountEmail.Equals(email));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }


        public static async Task AddAccount(SystemAccount account)
		{
			try
			{
				using var db = new FunewsManagementContext();
				await db.SystemAccounts.AddAsync(account);
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public static async Task UpdateAccount(SystemAccount account)
		{
			try
			{
				using var db = new FunewsManagementContext();
				db.SystemAccounts.Update(account);
				await db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		public static async Task<SystemAccount> GetAccountByEmailAndPassword(string email, string password)
		{
			try
			{
				using var db = new FunewsManagementContext();
				return await db.SystemAccounts.FirstOrDefaultAsync(a => a.AccountEmail.Equals(email) && a.AccountPassword.Equals(password));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return null;
			}
		}

		public static async Task<SystemAccount> GetAccountById(int idAccount)
		{
			try
			{
				using var db = new FunewsManagementContext();
				return await db.SystemAccounts.FirstOrDefaultAsync(a => a.AccountId == idAccount);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return null;
			}
		}
        public static async Task<string> GetNextAccountIdAsync()
        {
            await using var db = new FunewsManagementContext();

            // Lấy danh sách ID từ database và chuyển đổi sang số nguyên
            var idList = await db.SystemAccounts
                                 .Select(a => a.AccountId)
                                 .ToListAsync();

            // Chuyển đổi danh sách ID từ string -> int, bỏ qua giá trị không hợp lệ
            var validIds = idList.Select(id => (int)id); // Chuyển short -> int

            // Lấy ID lớn nhất, cộng 1
            int nextId = validIds.Any() ? validIds.Max() + 1 : 1;

            // Trả về ID mới dưới dạng string
            return nextId.ToString();
        }

        public static async Task DeleteAccount(SystemAccount account)
        {
            await using var db = new FunewsManagementContext();

            if (account != null)
            {
                db.SystemAccounts.Remove(account);
                await db.SaveChangesAsync(); // Lưu thay đổi vào DB
            }
        }

    }
}
