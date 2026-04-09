using prakt15.data.repositories.TagsRepository;
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
    /// Логика взаимодействия для SelectTags.xaml
    /// </summary>
    public partial class SelectTags : Page
    {
        public Product product { get; set; }
        public Tags tags { get; set; } = new();
        public ITags_Repository tagsService { get; set; } = new Tags_Repository();

        public SelectTags(Product product)
        {
            this.product = product;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            tagsService.AddTagsToProduct(tags, product);
            NavigationService.GoBack();
        }
    }
}
