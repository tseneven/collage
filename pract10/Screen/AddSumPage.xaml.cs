using System.Windows;
using System.Windows.Controls;
using pract10.Domain;
using pract10.Domain.Entites;

namespace pract10.Screen;

public partial class AddSumPage : Page
{
    public Credits Credits { get; set; } = new();
    public AddSumPage()
    {
        InitializeComponent();
    }

    private void Add_OnClick(object sender, RoutedEventArgs e)
    {
        var validatieResult = ValidateData(Credits);
        if (!validatieResult)
        {
            MessageBox.Show("АААААААА ДАННЫЕ НЕ ВСЕ ЗАПОЛНЕНЫ или сумма меньше нуля =)");
            return;
        }
        CreditsList.Instance.Credits.Add(Credits);
        NavigationService.GoBack();
    }

    private bool ValidateData(Credits credits)
    {
        if(string.IsNullOrEmpty(SumText.Text))
            return false;
        if(string.IsNullOrEmpty(CategoryText.Text))
            return false;
        if(string.IsNullOrEmpty(DescriptionText.Text))
            return false;
        if(string.IsNullOrEmpty(DatePicker.Text))
            return false;
        if(credits.Sum < 1)
            return false;
        return true;
    }
}