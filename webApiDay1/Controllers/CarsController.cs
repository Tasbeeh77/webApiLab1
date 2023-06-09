﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using webApiDay1.Filters;
using webApiDay1.Models;

namespace webApiDay1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        #region Cars collection

            private static List<Car> cars = new List<Car>
            {
                new Car { Id = 1, Name = "Toyota", Model = "Camry", ProductionDate = new DateTime(2019, 1, 1) },
                new Car { Id = 2, Name = "Honda", Model = "Accord", ProductionDate = new DateTime(2020, 2, 2) },
                new Car { Id = 3, Name = "Ford", Model = "Mustang", ProductionDate = new DateTime(2018, 3, 3) }
            };
        #endregion

        [HttpGet]
        #region Get all Cars Data
            public ActionResult<List<Car>> GetAll()
            {
                return cars; 
            }

        #endregion

        [HttpGet]
        [Route("{id}")]
        #region Get car data by id
            public ActionResult<Car> GetById(int id)
            {
                Car? Car = cars.FirstOrDefault(m => m.Id == id);
                if (Car is null)
                {
                    return NotFound(new GeneralResponse("Car isn't found"));
                }
                return Car; 
            }

        #endregion

        [HttpPost]
        [Route("Old")]
        #region add car deprecated version
            public ActionResult AddOld(Car car)
            {
                car.Id = cars.Count + 1;
                car.Type = "Gas";
                cars.Add(car);
                return CreatedAtAction("GetById", new { id = car.Id },
                new GeneralResponse("car has been added successfully"));
            }

        #endregion

        [HttpPost]
        [Route("New")]
        [ValidateCarType]
        #region add car new version
            public ActionResult AddNew(Car car)
            {
                car.Id = cars.Count + 1;
                cars.Add(car);
                return CreatedAtAction("GetById", new { id = car.Id },
                new GeneralResponse("car has been added successfully"));
            }
        #endregion

        [HttpPut]
        [Route("{id}")]
        [ValidateCarType]
        #region Update cars data
            public ActionResult Update(Car CarFromRequest, int id)
            {
                if (id != CarFromRequest.Id)
                {
                    return BadRequest(new GeneralResponse("This car doesn't Exist"));
                }
                Car? CarToEdit = cars.FirstOrDefault(m => m.Id == id);
                if (CarToEdit is null)
                {
                    return NotFound();
                }
                CarToEdit.Name = CarFromRequest.Name;
                CarToEdit.Model = CarFromRequest.Model;
                CarToEdit.ProductionDate = CarFromRequest.ProductionDate;
                CarToEdit.Type = CarFromRequest.Type;
                return NoContent();
            }
        #endregion

        [HttpDelete] 
        [Route("{id}")]
        #region Delete car
            public ActionResult Delete(int id)
            {
                Car? CarToDelete = cars.FirstOrDefault(m => m.Id == id);
                if (CarToDelete is null)
                {
                    return NotFound();
                }
                cars.Remove(CarToDelete);
                return NoContent();
            }

        #endregion

        [HttpGet]
        [Route("GetCount")]
        #region Get Requests count
            public IActionResult GetRequestsCount()
            {
                return Ok(Program.requestsCount.ToString());
            }
        #endregion

/*  [HttpGet]
    [Route("GetCount")]
    public IActionResult GetRequestsCount()
    {
        if (HttpContext.Response.Headers.TryGetValue("Request-Count", out var values))
        {
            return Ok(values.First().ToString());
        }
        return NotFound(); 
    }*/
    }
}
