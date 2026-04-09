using prakt15.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.data.repositories.CategoriesRepository
{
    public class Categories_Repository : ICategories_Repository
    {
        public ObservableCollection<Categories> Categories { get; set; } = new();
        private readonly AppDBContext _db = Base_Repository.Instance.Context;

        public Categories_Repository()
        {
            GetAll();
        }
        public void GetAll()
        {
            Categories.Clear();
            var categories = _db.Categories.ToList();
            foreach (Categories category in categories) 
            {
                Categories.Add(category);
            }
        }

        public void AddCategories(Categories categories)
        {
            var _categories = new Categories
            {
                Name = categories.Name,
            };
            _db.Categories.Add(categories);
            Categories.Add(categories);
            Commit();
        }

        public void EditCategories()
        {
            Commit();
        }

        public void DeleteCategories(Categories categories)
        {
            _db.Categories.Remove(categories);
            if (Commit() > 0)
                if (Categories.Contains(categories))
                {
                    Categories.Remove(categories);
                    GetAll();
                }

        }
        public int Commit() => _db.SaveChanges();

    }
}
