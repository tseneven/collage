using prakt15.models;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Linq;

namespace prakt15.data.repositories.TagsRepository
{
    public class Tags_Repository : ITags_Repository
    {
        private readonly AppDBContext _db = Base_Repository.Instance.Context;   
        public ObservableCollection<Tags> Tags { get; set; } = new();
        public Tags_Repository()
        {
            GetAll();
        }

        public void GetAll()
        {
            Tags.Clear();
            var tags = _db.Tags.ToList();
            foreach (Tags tag in tags)
            {
                Tags.Add(tag);
            }
        }

        public void DeleteTags(Tags tags)
        {
            _db.Tags.Remove(tags);
            if (Commit() > 0)
                if (Tags.Contains(tags))
                {
                    Tags.Remove(tags);
                    GetAll();
                }
        }

        public void EditTags()
        {
            Commit();
        }

        public void AddTags(Tags tags)
        {
            var _tags = new Tags
            {
                Name = tags.Name,
            };
            _db.Tags.Add(_tags);
            Tags.Add(_tags);
            Commit();
        }
        public int Commit() => _db.SaveChanges();

        public void AddTagsToProduct(Tags tags, Product product)
        {
            var _tagsProduct = new ProductsTags
            {
                ProductId = product.Id,
                TagsId = tags.Id
            };
            _db.ProductsTags.Add(_tagsProduct);
            Commit();
        }
    }
}
