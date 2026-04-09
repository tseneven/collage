using prakt15.models;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;

namespace prakt15.data.repositories.BrandsRepository
{
    public class Brand_Repository : IBrand_Repository
    {
        private readonly AppDBContext _db = Base_Repository.Instance.Context;   
        public ObservableCollection<Brand> Brands { get; set; } = new();
        public Brand_Repository()
        {
            GetAll();
        }

        public void GetAll()
        {
            Brands.Clear();
            var brands = _db.Brands.ToList();
            foreach (Brand brand in brands)
            {
                Brands.Add(brand);
            }
        }

        public void DeleteBrand(Brand brand)
        {
            _db.Brands.Remove(brand);
            if (Commit() > 0)
                if (Brands.Contains(brand))
                {
                    Brands.Remove(brand);
                    GetAll();
                }
        }

        public void EditBrand()
        {
            Commit();
        }

        public void AddBrand(Brand brand)
        {
            var _brand = new Brand
            {
                Name = brand.Name,
            };
            _db.Brands.Add(brand);
            Brands.Add(brand);
            Commit();
        }
        public int Commit() => _db.SaveChanges();

    }
}
