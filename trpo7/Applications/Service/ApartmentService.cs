using System.Text.Json;
using trpo7.Application.Helpers.ApiClient;
using trpo7.Application.Model;

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
}