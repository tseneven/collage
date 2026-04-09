using prakt15.data.repositories.BrandsRepository;
using prakt15.models;
using prakt15.pages.brand;
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
    /// Логика взаимодействия для BrandsPage.xaml
    /// </summary>
    public partial class BrandsPage : Page
    {
        public IBrand_Repository brandService { get; set; } = new Brand_Repository();
        public Brand brand { get; set; } = new();
        public BrandsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                mainWindow.Title = "Список брендов";
            }
            brandService.GetAll();
        }

        private void EditBrand_Click(object sender, RoutedEventArgs e)
        {
            if (brand != null)
            {
                NavigationService.Navigate(new AddAndEditBrandPage(brand));
            }
            else
                MessageBox.Show("Выберите бренд");
        }

        private void DeleteBrand_Click(object sender, RoutedEventArgs e)
        {
            if (brand != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    brandService.DeleteBrand(brand);
                }
            }
            else
                MessageBox.Show("Выберите бренд");
        }

        private void AddBrand_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAndEditBrandPage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
