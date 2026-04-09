using prakt15.data.repositories.BrandsRepository;
using prakt15.data.repositories.CategoriesRepository;
using prakt15.data.repositories.ProductsRepository;
using prakt15.models;
using System.Windows;
using System.Windows.Controls;

namespace prakt15.pages
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Page
    {
        public IBrand_Repository brandService { get; set; } = new Brand_Repository();
        public ICategories_Repository categoriesSeivice { get; set; } = new Categories_Repository();
        public IProduct_Repository productSerivce { get; set; } = new Product_Repository();
        private List<Product> allProducts = new();
        public List<Product> products { get; set; } = new();
        public int productCount;
        IQueryable<Product> query;
        public Catalog()
        {
            InitializeComponent();
            allProducts = productSerivce.Products.ToList();
            products = productSerivce.Products.ToList();
            productCount = productSerivce.Products.Count();
            inDB.Text = $"Всего товаров: {productCount.ToString()}";
            inList.Text = $"Всего показано: {productCount.ToString()}";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                mainWindow.Title = "Каталог товаров";
            }
            SortComboBox.ItemsSource = new List<string>()
            {
                "По названию",
                "По цене (убывание)",
                "По цене (возрастание)",
                "По количеству (убывание)",
                "По количеству (возрастание)"
            };
            UpdateProductsList();
        }

        private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyCurrentFiltersAndSort(true);
            UpdateProductsList();
            inList.Text = $"Всего показано: {products.Count}";
        }
        private void ApplyCurrentFiltersAndSort(bool isSortAndSearch)
        {
            bool CBCorrect = false;
            bool PriceCorrect = false;

            if (isSortAndSearch)
            {
                CBCorrect = true;
                PriceCorrect = true;
            }
            else
            {
                if (CategoriesCB.SelectedIndex != -1)
                {
                    CBCorrect = true;
                }

                if (BrandCB.SelectedIndex != -1)
                {
                    CBCorrect = true;
                }

                if (int.TryParse(toPrice.Text, out int too) || int.TryParse(fromPrice.Text, out int fr))
                {
                    PriceCorrect = true;
                }
                if (!CBCorrect && !PriceCorrect)
                {
                    MessageBox.Show("Ошибка");
                    return;
                }


            }



            IQueryable<Product> query = allProducts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                string searchTerm = SearchTextBox.Text.ToLower().Trim();
                query = query.Where(p => p.Name.ToLower().Contains(searchTerm));
            }

            if (CategoriesCB.SelectedItem is Categories selectedCategory && selectedCategory != null)
            {
                query = query.Where(p => p.Categories != null &&
                                         p.Categories.Name == selectedCategory.Name);
            }

            if (BrandCB.SelectedItem is Brand selectedBrand && selectedBrand != null)
            {
                query = query.Where(p => p.Brand != null &&
                                         p.Brand.Name == selectedBrand.Name);
            }

            if (double.TryParse(toPrice.Text.Trim(), out double priceFrom) && priceFrom > 0)
            {
                query = query.Where(p => p.Price >= priceFrom);
            }

            if (double.TryParse(fromPrice.Text.Trim(), out double priceTo) && priceTo > 0)
            {
                query = query.Where(p => p.Price <= priceTo);
            }

            if (SortComboBox.SelectedItem == null)
            {
                products = query.ToList();
                return;
            }


            string selectedSort = SortComboBox.SelectedItem.ToString();

            switch (selectedSort)
            {
                case "По названию":
                    products = query.OrderBy(x => x.Name).ToList();
                    break;

                case "По цене (убывание)":
                    products = query.OrderByDescending(x => x.Price).ToList();
                    break;

                case "По цене (возрастание)":
                    products = query.OrderBy(x => x.Price).ToList();
                    break;

                case "По количеству (убывание)":
                    products = query.OrderByDescending(x => x.Stock).ToList();
                    break;

                case "По количеству (возрастание)":
                    products = query.OrderBy(x => x.Stock).ToList();
                    break;

                default:
                    products = query.ToList();
                    break;
            }
        }
        private void UpdateProductsList()
        {
            if (listViewProducts != null)
            {
                listViewProducts.ItemsSource = null;
                listViewProducts.ItemsSource = products;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyCurrentFiltersAndSort(true);
            UpdateProductsList();
            inList.Text = $"Всего показано: {products.Count}";
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            ApplyCurrentFiltersAndSort(false);
            UpdateProductsList();
            inList.Text = $"Всего показано: {products.Count}";
        }
    }
}
