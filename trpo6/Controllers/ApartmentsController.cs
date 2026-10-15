using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
    public IActionResult Post([FromBody] ApartmentDTO apartmentModel)
    {
        ValidationResult validation =  ValidationApartmentModel(apartmentModel);
        
        if(validation.succses == false)
            return BadRequest(validation.error);
        var apartment = new Apartment
        {
            Address = validation.model.Address,
            Rooms = validation.model.Rooms,
            Area = validation.model.Area,
            Price = validation.model.Price,
            IsAvailable = validation.model.IsAvailable,
        };

        var apartmentEntity = entityStorage.CreateEntity(apartment);
        if(apartmentEntity == null)
            return StatusCode(500, "Сервер навернулся?");
        return Created();
    }

    [HttpPatch("{id:int}")]
    public IActionResult Patch([FromRoute] int id, [FromBody] ApartmentDTO apartmentModel)
    {
        var apartment = entityStorage
            .Select<Apartment>()
            .SingleOrDefault(x => x.Id == id);

        if (apartment == null)
            return NotFound();

        if (apartmentModel.Address != null)
        {
            if (string.IsNullOrWhiteSpace(apartmentModel.Address))
                return BadRequest("Адрес не может быть пустым");

            apartment.Address = apartmentModel.Address;
        }

        if (apartmentModel.Rooms != null)
        {
            if (!int.TryParse(apartmentModel.Rooms, out int rooms))
                return BadRequest("Rooms: неверный тип данных");

            if (rooms <= 0)
                return BadRequest("Количество комнат должно быть больше нуля");

            apartment.Rooms = rooms;
        }

        if (apartmentModel.Area != null)
        {
            if (!decimal.TryParse(
                    apartmentModel.Area,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal area))
                return BadRequest("Area: неверный тип данных");

            if (area <= 0)
                return BadRequest("Площадь должна быть больше нуля");

            apartment.Area = area;
        }

        if (apartmentModel.Price != null)
        {
            if (!decimal.TryParse(
                    apartmentModel.Price,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal price))
                return BadRequest("Price: неверный тип данных");

            if (price <= 0)
                return BadRequest("Цена должна быть больше нуля");

            apartment.Price = price;
        }

        if (apartmentModel.IsAvailable != null)
        {
            if (!bool.TryParse(apartmentModel.IsAvailable, out bool isAvailable))
                return BadRequest("IsAvailable: неверный тип данных");

            apartment.IsAvailable = isAvailable;
        }

        entityStorage.Patch();

        return Ok(apartment);
    }
    
    [HttpPut, Route("{id:int}")]
    public IActionResult Put([FromRoute] int id, [FromBody] ApartmentDTO apartmentModel)
    {
        var validation = ValidationApartmentModel(apartmentModel);
        if (!validation.succses) 
            return BadRequest(validation.error);

        var apartment = entityStorage.Select<Apartment>().SingleOrDefault(x => x.Id == id);
        if (apartment == null) 
            return NotFound();

        apartment.Address = validation.model.Address;
        apartment.Rooms = validation.model.Rooms;
        apartment.Area = validation.model.Area;
        apartment.Price = validation.model.Price;
        apartment.IsAvailable = validation.model.IsAvailable;

        var updatedEntity = entityStorage.PutEntity(apartment);
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
    
    private ValidationResult ValidationApartmentModel(ApartmentDTO apartmentModel)
    {

        if (!decimal.TryParse(
                apartmentModel.Price,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal decimalPrice))
            return new ValidationResult(succses: false, error: "Price Неверный тип данных", model: null);
        if (decimalPrice <= 0)
            return new ValidationResult(succses: false, error: "Цена не может быть меньше 1", model: null);
        
        if (!decimal.TryParse(
                apartmentModel.Area,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal decimalArea))
            return new ValidationResult(succses: false, error: "Area Неверный тип данных", model: null);
        if (decimalArea <= 0)
            return new ValidationResult(succses: false, error: "Площадь не может быть меньше 1", model: null);
        
        if (!int.TryParse(apartmentModel.Rooms, out int intRooms))
            return new ValidationResult(succses: false, error: "Rooms Неверный тип данных", model: null);
        if (intRooms <= 0)
            return new ValidationResult(succses: false, error: "Количество комнат не может быть меньше 1", model: null);
        
        if (string.IsNullOrEmpty(apartmentModel.Address))
            return new ValidationResult(succses: false, error: "Address Адрес не может быть пустым", model: null);

        if (!bool.TryParse(apartmentModel.IsAvailable, out bool boolIsAvailable))
            return new ValidationResult(succses: false, error: "IsAvailable Неверный тип данных", model: null);

        return new ValidationResult(succses: true, error: null, model: new Apartment
        {
            Address = apartmentModel.Address,
            Rooms = intRooms,
            Area = decimalArea,
            Price = decimalPrice,
            IsAvailable = boolIsAvailable
        });
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

public class ApartmentDTO
{
    public string? Address { get; set; }

    public string? Rooms { get; set; }

    public string? Area { get; set; }

    public string? Price { get; set; }

    public string? IsAvailable { get; set; }
}

public class ValidationResult
{
    public ValidationResult(bool succses, string? error, Apartment? model)
    {
        this.succses = succses;
        this.error = error;
        this.model = model;
    }

    public bool succses { get; set; }
    public string? error { get; set; }
    public Apartment? model { get; set; }
}
