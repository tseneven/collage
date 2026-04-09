using prakt15.data.repositories.BrandsRepository;
using prakt15.data.repositories.CategoriesRepository;
using prakt15.data.repositories.TagsRepository;
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
    public partial class TagsPage : Page
    {
        public ITags_Repository tagsService { get; set; } = new Tags_Repository();
        public Tags tag { get; set; } = new();
        public TagsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                mainWindow.Title = "Список тэгов";
            }
            tagsService.GetAll();
        }

        private void EditBrand_Click(object sender, RoutedEventArgs e)
        {
            if (tag != null)
            {
                NavigationService.Navigate(new AddAndEditTagsPage(tag));
            }
            else
                MessageBox.Show("Выберите тэг");
        }

        private void DeleteBrand_Click(object sender, RoutedEventArgs e)
        {
            if (tag != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    tagsService.DeleteTags(tag);
                }
            }
            else
                MessageBox.Show("Выберите тэг");
        }

        private void AddBrand_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAndEditTagsPage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
