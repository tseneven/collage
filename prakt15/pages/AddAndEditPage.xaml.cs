using prakt15.data.repositories;
using prakt15.data.repositories.BrandsRepository;
using prakt15.data.repositories.CategoriesRepository;
using prakt15.data.repositories.ProductsRepository;
using prakt15.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prakt15.pages
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditPage.xaml
    /// </summary>
    public partial class AddAndEditPage : Page
    {
        public IProduct_Repository productSerivce { get; set; } = new Product_Repository();
        public IBrand_Repository brandService { get; set; } = new Brand_Repository();
        public ICategories_Repository categoriesSeivice { get; set; } = new Categories_Repository();
        public Product product { get; set; } = new();
        bool isEdit = false;

        public AddAndEditPage()
        {
            InitializeComponent();
        }
        public AddAndEditPage(Product product)
        {
            this.product = product;
            InitializeComponent();
            Label.Text = "Изменить";
            Add.Content = "Изменить";
            isEdit = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                if(!isEdit)
                    mainWindow.Title = "Добавление товара";
                else
                    mainWindow.Title = "Изменение товара";
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
                productSerivce.EditProduct();
            else
                productSerivce.AddProduct(product);
            NavigationService.GoBack(); 
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
