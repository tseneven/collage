using prakt15.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prakt15.models
{
    public class ProductsTags : ObservableObject
    {
        private int _productId;
        private int _tagsId;
        private Product _product;
        private Tags _tags;

        public int ProductId { get => _productId; set => SetProperty(ref _productId, value); }
        public int TagsId { get => _tagsId; set => SetProperty(ref _tagsId, value); }
        public Product Product { get => _product; set => SetProperty(ref _product, value); }
        public Tags Tags { get => _tags; set => SetProperty(ref _tags, value); }
    }
}
