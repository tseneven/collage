using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using trpo7.Application.Service;

namespace trpo7.Screen.Pages;

public partial class CreateAndUpdatePage : Page, INotifyPropertyChanged
{
    private readonly ApartmentService _apartmentService;
    private readonly int? _apartmentId;

    public string SaveButtonText => _apartmentId.HasValue ? "Сохранить" : "Добавить";
    
    private ApartmentModel _apartment = new();

    public ApartmentModel Apartment
    {
        get => _apartment;
        set
        {
            _apartment = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    

    public CreateAndUpdatePage(ApartmentService apartmentService, int? apartmentId)
    {
        _apartmentService = apartmentService;
        _apartmentId = apartmentId;
        InitializeComponent();
        if (_apartmentId.HasValue)
            Loaded += CreateAndUpdatePage_OnLoaded;
    }

    private async void CreateAndUpdatePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= CreateAndUpdatePage_OnLoaded;
        IsEnabled = false;
        try
        {
            await Get(_apartmentId!.Value);
        }
        catch (Exception ex)
        {
            MainSnackbar.MessageQueue?.Enqueue($"Error: {ex.Message}");
        }
        finally
        {
            IsEnabled = true;
        }
    }
    
    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            ValidateData(Apartment);
            if (_apartmentId.HasValue)
                await _apartmentService.UpdateApartment(_apartmentId.Value, Apartment);
            else
                await _apartmentService.CreateApartment(Apartment);
            NavigationService.GoBack();
        }
        catch (Exception ex)
        {
            MainSnackbar.MessageQueue?.Enqueue($"Error: {ex.Message}");
        }
    }

    private void ValidateData(ApartmentModel model)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(model.Address))
        {
            errors.Add("Адрес не может быть пустым.");
        }

        if (model.Rooms <= 0)
        {
            errors.Add("Количество комнат должно быть больше 0.");
        }

        if (model.Area <= 0)
        {
            errors.Add("Площадь квартиры должна быть больше 0.");
        }

        if (model.Price <= 0)
        {
            errors.Add("Цена должна быть больше 0.");
        }

        if (errors.Any())
            throw new InvalidOperationException($"Валидация ну как-бы сё...{string.Join(',', errors)}");
    }

    private void ButtonBase1_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }

    private async Task Get(int apartmentId)
    {
        var apr = await _apartmentService.GetApartment(apartmentId);
        Apartment = apr.ToModel();
    }
}
