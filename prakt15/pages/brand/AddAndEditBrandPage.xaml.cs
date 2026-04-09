using prakt15.data.repositories.BrandsRepository;
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
    /// Логика взаимодействия для AddAndEditBrandPage.xaml
    /// </summary>
    public partial class AddAndEditBrandPage : Page
    {
        public Brand brand { get; set; } = new();
        bool isEdit = false;
        public IBrand_Repository brandService { get; set; } = new Brand_Repository();

        public AddAndEditBrandPage()
        {
            InitializeComponent();
        }
        public AddAndEditBrandPage(Brand brand)
        {
            this.brand = brand;
            isEdit = true;
            InitializeComponent();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                if (!isEdit)
                    mainWindow.Title = "Добавление бренда";
                else
                    mainWindow.Title = "Изменение бренда";
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!isEdit)
                brandService.AddBrand(brand);
            else
                brandService.EditBrand();
            NavigationService.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
