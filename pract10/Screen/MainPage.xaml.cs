using System.Windows;
using System.Windows.Controls;
using pract10.Domain;

namespace pract10.Screen;

public partial class MainPage : Page
{
    public string FilterString { get; set; } = "";
    public MainPage()
    {
        InitializeComponent();
        CreditsGrid.ItemsSource = CreditsList.Instance.Credits;
    }

    private void AddSum_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new AddSumPage());
    }

    private void ExportToCsv_OnClick(object sender, RoutedEventArgs e)
    {
        ExportCreator creator = new CsvExportCreator();
        creator.Export(CreditsList.Instance.Credits.ToList());
        MessageBox.Show("CSV экспортирован на рабочий стол");
    }

    private void ExportToJson_OnClick(object sender, RoutedEventArgs e)
    {
        ExportCreator creator = new JsonExportCreator();
        creator.Export(CreditsList.Instance.Credits.ToList());
        MessageBox.Show("Json экспортирован на рабочий стол");
    }

    private void FilterText_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (CreditsGrid is null)
            return;

        string text = ((TextBox)sender).Text;

        CreditsGrid.ItemsSource = string.IsNullOrEmpty(text)
            ? CreditsList.Instance.Credits
            : CreditsList.Instance.GetFilteredCredits(text);
    }
}