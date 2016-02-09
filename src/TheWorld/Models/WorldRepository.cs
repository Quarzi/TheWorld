using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private ILogger<WorldRepository> _logger;
        private WorldContext _worldContext;

        public WorldRepository(WorldContext worldContext, ILogger<WorldRepository> logger)
        {
            _worldContext = worldContext;
            _logger = logger;
        }

        public void AddStop(string tripName, Stop newStop, string userName)
        {
            try
            {
                var theTrip = GetTripByName(tripName, userName);
                newStop.Order = theTrip.Stops.Max(s => s.Order) + 1;
                _worldContext.Stops.Add(newStop);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database", ex);
            }
        }

        public void AddTrip(Trip newTrip)
        {
            try
            {
                _worldContext.Add(newTrip);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database", ex);
            }
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _worldContext.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips from database", ex);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _worldContext.Trips
                .Include(t => t.Stops)
                .OrderBy(t => t.Name)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips with stops from database", ex);
                return null;
            }
        }

        public Trip GetTripByName(string tripName, string userName)
        {
            try
            {
                return _worldContext.Trips
                        .Include(t => t.Stops)
                        .Where(t => t.Name == tripName && t.UserName == userName)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trip with stops by name from database", ex);
                return null;
            }
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string userName)
        {
            try
            {
                return _worldContext.Trips 
                .Include(t => t.Stops)
                .OrderBy(t => t.Name)
                .Where(t => t.UserName == userName)
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get trips for user with stops from database", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _worldContext.SaveChanges() > 0;
        }
    }
}
