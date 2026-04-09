using prakt15.data.repositories.BrandsRepository;
using prakt15.data.repositories.CategoriesRepository;
using prakt15.data.repositories.ProductsRepository;
using prakt15.models;
using System.Windows;
using System.Windows.Controls;

namespace prakt15.pages
{
    /// <summary>
    /// Логика взаимодействия для MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        public IBrand_Repository brandService { get; set; } = new Brand_Repository();
        public ICategories_Repository categoriesSeivice { get; set; } = new Categories_Repository();
        public IProduct_Repository productSerivce { get; set; } = new Product_Repository();
        private List<Product> allProducts = new();
        public List<Product> products { get; set; } = new();
        public int productCount;
        IQueryable<Product> query;
        public Product product { get; set; }


        public MainAdminPage()
        {
            InitializeComponent();
            allProducts = productSerivce.Products.ToList();
            products = productSerivce.Products.ToList();
            productCount = productSerivce.Products.Count();
            inDB.Text = $"Всего товаров: {productCount.ToString()}";
            inList.Text = $"Всего показано: {products.Count().ToString()}";
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
            ApplyCurrentFiltersAndSort();
            UpdateProductsList();
            inList.Text = $"Всего показано: {products.Count}";
        }
        private void ApplyCurrentFiltersAndSort()
        {
            if (CategoriesCB.SelectedIndex == -1 && BrandCB.SelectedIndex == -1 && (int.TryParse(toPrice.Text, out int to1) && int.TryParse(fromPrice.Text, out int fr1)))
            {
                MessageBox.Show("Введены некорректные значения");
                return;
            }

            if (!int.TryParse(toPrice.Text, out int to) && !int.TryParse(fromPrice.Text, out int fr))
            {
                MessageBox.Show("Введены некорректные значения");
                return;
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
            productCount = productSerivce.Products.Count();
            inDB.Text = $"Всего товаров: {productCount.ToString()}";
            inList.Text = $"Всего показано: {products.Count().ToString()}";
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAndEditPage());
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (product != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    productSerivce.DeleteProduct(product);
                    products.Remove(product);
                    allProducts.Remove(product);
                    UpdateProductsList();
                }
            }
            else
                MessageBox.Show("Выберите товар");
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (product != null)
                NavigationService.Navigate(new AddAndEditPage(product));
            else
                MessageBox.Show("Выберите товар");

        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyCurrentFiltersAndSort();
            UpdateProductsList();
            inList.Text = $"Всего показано: {products.Count}";
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            ApplyCurrentFiltersAndSort();
            UpdateProductsList();
            inList.Text = $"Всего показано: {products.Count}";
        }

        private void Brand_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BrandsPage());
        }

        private void category_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CategoriesPage());
        }

        private void Tags_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TagsPage());
        }

        private void addTags_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Product product)
            {
                NavigationService.Navigate(new SelectTags(product));
            }
        }
    }
}
