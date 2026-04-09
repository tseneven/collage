using prakt15.data.utils;
using System.Collections.ObjectModel;

namespace prakt15.models
{
    public class Product : ObservableObject
    {
        private int _id;
        private string _name;
        private string _description;
        private double _price;
        private int _stock;
        private double _rating;
        private DateTime _created_at;
        private int _category_id;
        private int _brand_id;
        private Brand _brand;
        private Categories _categories;
        private ObservableCollection<ProductsTags> _productsTags;

        public int Id { get => _id; set => SetProperty(ref _id, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public string Description { get => _description; set => SetProperty(ref _description, value); }
        public double Price { get => _price; set => SetProperty(ref _price, value); }
        public int Stock { get => _stock; set { if (SetProperty(ref _stock, value)) { OnPropertyChanged(nameof(AlmostOutOfStock)); } } }
        public double Rating { get => _rating; set => SetProperty(ref _rating, value); }
        public DateTime Created_at { get => _created_at; set => SetProperty(ref _created_at, value); }
        public int Category_id { get => _category_id; set => SetProperty(ref _category_id, value); }
        public int Brand_id { get => _brand_id; set => SetProperty(ref _brand_id, value); }
        public Brand Brand { get => _brand; set => SetProperty(ref _brand, value); }
        public Categories Categories { get => _categories; set => SetProperty(ref _categories, value); }
        public ObservableCollection<ProductsTags> ProductsTags { get => _productsTags; set => SetProperty(ref _productsTags, value); }
        public bool AlmostOutOfStock => Stock < 10;
    }
}
