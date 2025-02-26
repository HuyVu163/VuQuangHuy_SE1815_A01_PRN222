using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuQuangHuyMVC.Models;

namespace Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService()
        {
            _tagRepository = new TagRepository();
        }

        public async Task AddTag(Tag tag)
        {
            await _tagRepository.AddTag(tag);
        }

        public async Task DeleteTag(Tag tag)
        {
            await _tagRepository.DeleteTag(tag);

        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _tagRepository.GetAll();
        }

        public async Task<Tag> GetTagById(int idTag)
        {
            return await _tagRepository.GetTagById(idTag);
        }

        public async Task UpdateTag(Tag tag)
        {
            await _tagRepository.UpdateTag(tag);

        }
    }
}
