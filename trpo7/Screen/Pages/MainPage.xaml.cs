using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using trpo7.Application.Model;
using trpo7.Application.Service;
using trpo7.Screen.Pages;

namespace trpo7.Screen;

public partial class MainPage : Page
{
    private readonly ApartmentService _apartmentService;
    public ObservableCollection<Apartment> Apartments { get; } = new();
    public Apartment? SelectedApartment { get; set; }

    public MainPage(ApartmentService apartmentService)
    {
        InitializeComponent();
        _apartmentService = apartmentService;

        Init().GetAwaiter();
    }

    private async Task Init()
    {
        try
        {
            var apartmentsList = await _apartmentService.GetApartments();
            Apartments.Clear();

            foreach (var apartment in apartmentsList)
            {
                Apartments.Add(apartment);
            }
        }
        catch (Exception ex)
        {
            MainSnackbar.MessageQueue?.Enqueue("Error: " + ex.Message);
        }
    }

    private void AddApartment_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new CreateAndUpdatePage(_apartmentService, null));
    }

    private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        => await Init();


    private async void DeleteApartment_OnClick(object sender, RoutedEventArgs e)
    {
        if (SelectedApartment == null)
            MainSnackbar.MessageQueue?.Enqueue("Для начала нужно выбрать апартаменты");
        else
            await _apartmentService.DeleteApartment(SelectedApartment.Id);
        await Init();
    }

    private void EditApartment_OnClick(object sender, RoutedEventArgs e)
    {
        if (SelectedApartment == null)
            MainSnackbar.MessageQueue?.Enqueue("Для начала нужно выбрать апартаменты");
        else
            NavigationService.Navigate(new CreateAndUpdatePage(_apartmentService, SelectedApartment.Id));
    }
}