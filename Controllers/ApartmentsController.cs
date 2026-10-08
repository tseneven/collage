using Microsoft.AspNetCore.Mvc;
using trpo6.Infrastructure.MSSQL;
using trpo6.Models;

namespace trpo6.Controllers;

[ApiController, Route("api/v1/[controller]")]
public class ApartmentsController(EntityStorage entityStorage) : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        var aps = entityStorage.Select<Apartment>().ToArray();

        if (aps.Length == 0)
            return NotFound();

        return Ok(aps);
    }

    [HttpGet, Route("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        var aps = entityStorage
            .Select<Apartment>()
            .SingleOrDefault(x => x.Id == id);

        if (aps == null)
            return NotFound();

        return Ok(aps);
    }

    [HttpPost]
    public IActionResult Post([FromBody] ApartmentModel apartmentModel)
    {
        var validation =  ValidationApartmentModel(apartmentModel);
        
        if(validation.succses == false)
            return BadRequest(validation.error);
        var apartment = new Apartment
        {
            Address = apartmentModel.Address,
            Rooms = apartmentModel.Rooms,
            Area = apartmentModel.Area,
            Price = apartmentModel.Price,
            IsAvailable = apartmentModel.IsAvailable,
        };

        var apartmentEntity = entityStorage.CreateEntity(apartment);
        if(apartmentEntity == null)
            return StatusCode(500, "Сервер навернулся?");
        return Created();
    }

    [HttpPatch, Route("{id:int}")]
    public IActionResult Patch([FromRoute] int id, [FromBody] ApartmentModel apartmentModel)
    {
        var validation = ValidationApartmentModel(apartmentModel);
        if (!validation.succses) 
            return BadRequest(validation.error);

        var apartment = entityStorage.Select<Apartment>().SingleOrDefault(x => x.Id == id);
        if (apartment == null) 
            return NotFound();

        var patchedEntity = entityStorage.PatchEntity(apartment, apartmentModel.ToModel());
        if (patchedEntity == null) 
            return StatusCode(500, "Ошибка при частичном обновлении");

        return Ok(patchedEntity);
    }
    
    [HttpPut, Route("{id:int}")]
    public IActionResult Put([FromRoute] int id, [FromBody] ApartmentModel apartmentModel)
    {
        var validation = ValidationApartmentModel(apartmentModel);
        if (!validation.succses) 
            return BadRequest(validation.error);

        var apartment = entityStorage.Select<Apartment>().SingleOrDefault(x => x.Id == id);
        if (apartment == null) 
            return NotFound();

        apartment.Address = apartmentModel.Address;
        apartment.Rooms = apartmentModel.Rooms;
        apartment.Area = apartmentModel.Area;
        apartment.Price = apartmentModel.Price;
        apartment.IsAvailable = apartmentModel.IsAvailable;

        var updatedEntity = entityStorage.UpdateEntity(apartment);
        if (updatedEntity == null) 
            return StatusCode(500, "Ошибка при сохранении данных на сервере");

        return Ok(updatedEntity);
    }
    
    [HttpDelete, Route("delete")]
    public IActionResult Delete([FromQuery] int id)
    {

        var apartment = entityStorage.Select<Apartment>().SingleOrDefault(x => x.Id == id);
        if (apartment == null) 
            return NotFound();
        
        var deleteEntity = entityStorage.DeleteEntity(apartment);
        if (deleteEntity == null) 
            return StatusCode(500, "Ошибка при сохранении данных на сервере");

        return NoContent();
    }


    private (bool succses, string? error) ValidationApartmentModel(ApartmentModel apartmentModel)
    {
        if (apartmentModel.Price <= 0)
            return (false, "Цена не может быть меньше 1");
        
        if (apartmentModel.Area <= 0)
            return (false,"Площадь не может быть меньше 1");
        
        if (apartmentModel.Rooms <= 0)
            return (false, "Количество комнат не может быть меньше 1");
        
        if (string.IsNullOrEmpty(apartmentModel.Address))
            return (false,"Адрес не может быть пустым");
        return (true, null);
    }
}

public class ApartmentModel
{
    public string Address { get; set; } = null!;

    public int Rooms { get; set; }

    public decimal Area { get; set; }

    public decimal Price { get; set; }

    public bool IsAvailable { get; set; }

    public Apartment ToModel() => new Apartment
    {
        Address = Address,
        Rooms = Rooms,
        Area = Area,
        Price = Price,
        IsAvailable = IsAvailable
    };
}