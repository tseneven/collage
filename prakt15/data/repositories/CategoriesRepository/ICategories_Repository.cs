using prakt15.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.data.repositories.CategoriesRepository
{
    public interface ICategories_Repository
    {
        public void GetAll();
        public void AddCategories(Categories categories);
        public void EditCategories();
        public void DeleteCategories(Categories categories);
    }
}
