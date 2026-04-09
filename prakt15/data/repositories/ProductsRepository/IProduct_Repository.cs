using prakt15.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.data.repositories.ProductsRepository
{
    public interface IProduct_Repository
    {
        public ObservableCollection<Product> Products { get;}
        public void GetAll();
        public void DeleteProduct(Product product);
        public void EditProduct();
        public void AddProduct(Product product);
    }
}
