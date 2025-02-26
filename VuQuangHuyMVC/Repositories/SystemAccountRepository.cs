using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Repositories
{
	public class SystemAccountRepository : ISystemAccountRepository
	{
        public async Task DeleteAccount(SystemAccount account)
        {
            await SystemAccountDAO.DeleteAccount(account);
        }

        public async Task<SystemAccount> GetAccountByEmail(string email)
        {
            return await SystemAccountDAO.GetAccountByEmail(email);
        }

        public async Task<string> GetNextAccountIdAsync()
        {
            return await SystemAccountDAO.GetNextAccountIdAsync();
        }

        async Task ISystemAccountRepository.AddAccount(SystemAccount account)
		{
			await SystemAccountDAO.AddAccount(account);
		}

		async Task<SystemAccount> ISystemAccountRepository.GetAccountByEmailAndPassword(string email, string password)
		{
			return await SystemAccountDAO.GetAccountByEmailAndPassword(email, password);
		}

		async Task<SystemAccount> ISystemAccountRepository.GetAccountById(int idAccount)
		{
			return await SystemAccountDAO.GetAccountById(idAccount);
		}

		async Task<IEnumerable<SystemAccount>> ISystemAccountRepository.GetAll()
		{
			return await SystemAccountDAO.GetAll();
		}

		async Task ISystemAccountRepository.UpdateAccount(SystemAccount account)
		{
			await SystemAccountDAO.UpdateAccount(account);
		}
	}
}
