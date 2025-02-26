using DataAccessLayer;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Services
{
	public class SystemAccountService : ISystemAccountService
	{
		private readonly ISystemAccountRepository _repository;

		public SystemAccountService()
		{
			_repository = new SystemAccountRepository();
		}

		public async Task AddAccount(SystemAccount account)
		{
			await _repository.AddAccount(account);
		}

        public async Task DeleteAccount(SystemAccount account)
        {
           await _repository.DeleteAccount(account);
        }

        public async Task<SystemAccount> GetAccountByEmail(string email)
        {
            return await _repository.GetAccountByEmail(email);
        }

        public async Task<SystemAccount> GetAccountByEmailAndPassword(string email, string password)
		{
			return await _repository.GetAccountByEmailAndPassword(email, password);
		}

		public async Task<SystemAccount> GetAccountById(int idAccount)
		{
			return await _repository.GetAccountById(idAccount);
		}

		public async Task<IEnumerable<SystemAccount>> GetAll()
		{
			return await _repository.GetAll();
		}

        public async Task<string> GetNextAccountIdAsync()
        {
            return await SystemAccountDAO.GetNextAccountIdAsync();
        }

        public async Task UpdateAccount(SystemAccount account)
		{
			await _repository.UpdateAccount(account);
		}
	}
}
