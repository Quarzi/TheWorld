using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using System.Net;
using AutoMapper;
using TheWorld.ViewModels;
using TheWorld.Services;
using Microsoft.AspNet.Authorization;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private CoordService _coordService;
        private ILogger<StopController> _logger;
        private IWorldRepository _worldRepository;

        public StopController(IWorldRepository worldRepository,
            ILogger<StopController> logger,
            CoordService coordService)
        {
            _worldRepository = worldRepository;
            _logger = logger;
            _coordService = coordService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _worldRepository.GetTripByName(tripName, User.Identity.Name);

                if (results == null)
                {
                    return Json(null);
                }

                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(s => s.Order)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stops for trip {tripName}", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occurred finding trip name");
            }
        }

        public JsonResult Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Map to Entity
                    var newStop = Mapper.Map<Stop>(vm);

                    // Looking up Geo-coordinates
                    var coordResult = _coordService.Lookup(newStop.Name);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordResult.Message);
                    }

                    newStop.Latitude = coordResult.Latitude;
                    newStop.Longitude = coordResult.Longitude;

                    // Save to db
                    _worldRepository.AddStop(tripName, newStop, User.Identity.Name);

                    if (_worldRepository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(Mapper.Map<StopViewModel>(newStop));
                    }
                }
                return Json(false);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new stop", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new stop");
            }
        }
    }
}
