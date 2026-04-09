using prakt15.data.repositories.BrandsRepository;
using prakt15.data.repositories.CategoriesRepository;
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
    public partial class CategoriesPage : Page
    {
        public ICategories_Repository categoriesService { get; set; } = new Categories_Repository();
        public Categories categories { get; set; } = new();
        public CategoriesPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                mainWindow.Title = "Список категорий";
            }
            categoriesService.GetAll();
        }

        private void EditBrand_Click(object sender, RoutedEventArgs e)
        {
            if (categories != null)
            {
                NavigationService.Navigate(new AddAndEditCategoriesPage(categories));
            }
            else
                MessageBox.Show("Выберите бренд");
        }

        private void DeleteBrand_Click(object sender, RoutedEventArgs e)
        {
            if (categories != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    categoriesService.DeleteCategories(categories);
                }
            }
            else
                MessageBox.Show("Выберите бренд");
        }

        private void AddBrand_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAndEditCategoriesPage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
