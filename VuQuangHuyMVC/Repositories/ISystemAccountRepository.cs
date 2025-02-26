using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Repositories
{
	public interface ISystemAccountRepository
	{
		Task<IEnumerable<SystemAccount>> GetAll();
		Task AddAccount(SystemAccount account);
		Task UpdateAccount(SystemAccount account);
		Task<SystemAccount> GetAccountByEmailAndPassword(string email, string password);
		Task<SystemAccount> GetAccountById(int idAccount);
		Task<string> GetNextAccountIdAsync();
		Task<SystemAccount> GetAccountByEmail(string email);
		Task DeleteAccount(SystemAccount account);
    }
}
