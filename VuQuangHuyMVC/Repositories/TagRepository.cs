using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Repositories
{
	public class TagRepository : ITagRepository
	{
		async Task ITagRepository.AddTag(Tag tag)
		{
			await TagDAO.AddTag(tag);
		}

		async Task ITagRepository.DeleteTag(Tag tag)
		{
			await TagDAO.DeleteTag(tag);
		}

		async Task<IEnumerable<Tag>> ITagRepository.GetAll()
		{
			return await TagDAO.GetAll();
		}

		async Task<Tag> ITagRepository.GetTagById(int idTag)
		{
			return await TagDAO.GetTagById(idTag);
		}

		async Task ITagRepository.UpdateTag(Tag tag)
		{
			await TagDAO.UpdateTag(tag);
		}
	}
}
