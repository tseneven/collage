using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using trpo7.Application.Model;
using trpo7.Applications.Helpers;
using trpo7.Applications.Helpers.ApiClient;

namespace trpo7.Application.Service;

public class ApartmentService
{
    public async Task<List<Apartment>> GetApartments()
    {
        var response = await ApiClient.Instance.Client.GetAsync("apartments");

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException("Сервер ничего не прислал =(");
        
        var json = await response.Content.ReadAsStringAsync();
        
        var apartmentsList = JsonSerializer.Deserialize<List<Apartment>>(json);

        if(apartmentsList == null)
            return new List<Apartment>();
        return apartmentsList;
    }
    
    public async Task<Apartment> GetApartment(int id)
    {
        var response = await ApiClient.Instance.Client.GetAsync($"apartments/{id}");

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException("Сервер ничего не прислал =(");
        
        var json = await response.Content.ReadAsStringAsync();
        
        var apartment = JsonSerializer.Deserialize<Apartment>(json);

        if(apartment == null)
            return new Apartment();
        return apartment;
    }
    
    public async Task CreateApartment(ApartmentModel requestModel)
    {
        var response = await ApiClient.Instance.Client.PostAsJsonAsync("apartments", requestModel.FromModel());

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Сервер ничего не прислал =( {response.StatusCode}");
    }

    public async Task UpdateApartment(int id, ApartmentModel requestModel)
    {
        var response = await ApiClient.Instance.Client.PutAsJsonAsync($"apartments/{id}", requestModel.FromModel());

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Сервер ничего не прислал =( {response.StatusCode}");
    }

    public async Task DeleteApartment(int id)
    {
        var response = await ApiClient.Instance.Client.DeleteAsync($"apartments/delete?id={id}");

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Сервер ничего не прислал =( {response.StatusCode}");
    }
}

public class ApartmentModel
{
    public string Address { get; set; } = string.Empty;
    public int Rooms { get; set; }
    public double Area { get; set; }
    public double Price { get; set; } 
    public bool IsAvailable { get; set; }
    
    public ApartmentDTO FromModel() => new()
    {
        Address = Address,
        Rooms = Rooms.ToString(),
        Area = Area.ToString(CultureInfo.InvariantCulture),
        Price = Price.ToString(CultureInfo.InvariantCulture),
        IsAvailable = IsAvailable.ToString()
    };
}
