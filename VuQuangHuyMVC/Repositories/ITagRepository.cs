using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Repositories
{
	public interface ITagRepository
	{
		Task<IEnumerable<Tag>> GetAll();
		Task<Tag> GetTagById(int idTag);
		Task AddTag(Tag tag);
		Task UpdateTag(Tag tag);
		Task DeleteTag(Tag tag);
	}
}
