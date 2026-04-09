using prakt15.data.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.models
{
    public class Categories : ObservableObject
    {
        private int _id;
        private string _name;
        private ObservableCollection<Product> _products;

        public int Id { get => _id; set => SetProperty(ref _id, value); }
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        public ObservableCollection<Product> Products { get => _products; set => SetProperty(ref _products, value); }
    }
}
