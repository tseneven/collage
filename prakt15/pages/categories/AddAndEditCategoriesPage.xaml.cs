using prakt15.data.repositories.BrandsRepository;
using prakt15.data.repositories.CategoriesRepository;
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

namespace prakt15.pages.brand
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditCategoriesPage.xaml
    /// </summary>
    public partial class AddAndEditCategoriesPage : Page
    {
        public Categories categories { get; set; } = new();
        bool isEdit = false;
        public ICategories_Repository categoriesService { get; set; } = new Categories_Repository();

        public AddAndEditCategoriesPage()
        {
            InitializeComponent();
        }
        public AddAndEditCategoriesPage(Categories categories)
        {
            this.categories = categories;
            isEdit = true;
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                if (!isEdit)
                    mainWindow.Title = "Добавление категории";
                else
                    mainWindow.Title = "Изменение категории";
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!isEdit)
                categoriesService.AddCategories(categories);
            else
                categoriesService.EditCategories();
            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
