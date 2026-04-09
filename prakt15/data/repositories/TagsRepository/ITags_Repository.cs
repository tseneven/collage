using prakt15.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.data.repositories.TagsRepository
{
    public interface ITags_Repository
    {
        public void GetAll();
        public void DeleteTags(Tags tags);
        public void EditTags();
        public void AddTags(Tags tags);
        public void AddTagsToProduct(Tags tags, Product product);
    }
}
