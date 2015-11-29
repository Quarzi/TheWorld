using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;
using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private IWorldRepository _worldRepository;

        public TripController(IWorldRepository worldRepository, ILogger<TripController> logger)
        {
            _worldRepository = worldRepository;
            _logger = logger;
        }

        // GET: api/values
        [HttpGet("")]
        public JsonResult Get()
        {
            var result = Mapper.Map<IEnumerable<TripViewModel>>(_worldRepository.GetAllTripsWithStops());
            return Json(result);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(vm);

                    // Save to db
                    _logger.LogInformation("Attempting to save a new trip");
                    _worldRepository.AddTrip(newTrip);

                    if (_worldRepository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new trip", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
