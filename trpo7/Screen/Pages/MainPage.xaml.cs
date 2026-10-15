using System.Collections.ObjectModel;
using System.Windows.Controls;
using trpo7.Application.Model;
using trpo7.Application.Service;

namespace trpo7.Screen;

public partial class MainPage : Page
{
    private readonly ApartmentService _apartmentService;
    public ObservableCollection<Apartment> Apartments { get; }  = new();
    
    public MainPage(ApartmentService apartmentService)
    {
        InitializeComponent();
        _apartmentService = apartmentService;

        Init().GetAwaiter();
    }

    private async Task Init()
    {
        
        var apartmentsList = await _apartmentService.GetApartments();
        Apartments.Clear();

        foreach (var apartment in apartmentsList)
        {
            Apartments.Add(apartment);
        }
        
    }
}