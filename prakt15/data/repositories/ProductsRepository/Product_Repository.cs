using Microsoft.EntityFrameworkCore;
using prakt15.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.data.repositories.ProductsRepository
{
    public class Product_Repository : IProduct_Repository
    {
        public ObservableCollection<Product> Products { get; set; } = new();
        private readonly AppDBContext _db = Base_Repository.Instance.Context;

        public Product_Repository() 
        {
            GetAll();
        }
        public void GetAll()
        {
            Products.Clear();
            var products = _db.Products.Include(p => p.ProductsTags).ThenInclude(p=>p.Tags).ToList();
            foreach(Product product in products)
            {
                Products.Add(product);
            }
        }

        public void DeleteProduct(Product product)
        {
            _db.Products.Remove(product);
            if (Commit() > 0)
                if (Products.Contains(product))
                {
                    Products.Remove(product);
                    GetAll();
                }

        }

        public void EditProduct()
        {
            Commit();
        }

        public void AddProduct(Product product)
        {
            var _product = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Created_at = DateTime.Now,
                Rating = product.Rating,
                Brand = product.Brand,
                Brand_id = product.Brand_id,
                Categories = product.Categories,
                Category_id = product.Category_id,
                ProductsTags = product.ProductsTags,
            };
            _db.Products.Add(_product);
            Commit();
            GetAll();
        }
        public int Commit() => _db.SaveChanges();
    }
}
