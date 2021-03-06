﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextSeedData
    {
        private WorldContext _worldContext;
        private UserManager<WorldUser> _userManager;

        public WorldContextSeedData(WorldContext worldContext, UserManager<WorldUser> userManager)
        {
            _worldContext = worldContext;
            _userManager = userManager;
        }

        public async Task EnsureSeedDataAsync()
        {
            if (await _userManager.FindByEmailAsync("kim.jan.andersen82@gmail.com") == null)
            {
                var newUser = new WorldUser()
                {
                    UserName = "kimandersen",
                    Email = "kim.jan.andersen82@gmail.com"
                };

                var user = await _userManager.CreateAsync(newUser, "1234Abcd");
            }

            if (!_worldContext.Trips.Any())
            {
                // Add new Data
                var usTrip1 = new Trip()
                {
                    Name = "Us Trip 1",
                    Created = DateTime.UtcNow,
                    UserName = "kimandersen",
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name = "Atlanta, Ga", Arrival = new DateTime(2014, 6, 4), Latitude = 33.748995, Longitude = -84.387982, Order = 0 },
                        new Stop() { Name = "New York, NY", Arrival = new DateTime(2014, 6, 4), Latitude = 40.712784, Longitude = -74.005941, Order = 1 },
                        new Stop() { Name = "Boston, MA", Arrival = new DateTime(2014, 6, 4), Latitude = 42.360082, Longitude = -71.058880, Order = 2 },
                        new Stop() { Name = "Chicago, IL", Arrival = new DateTime(2014, 6, 4), Latitude = 41.878114, Longitude = -87.629798, Order = 3 },
                        new Stop() { Name = "Seattle, WA", Arrival = new DateTime(2014, 6, 4), Latitude = 47.606209, Longitude = -122.332071, Order = 4 },
                        new Stop() { Name = "Atlanta, Ga", Arrival = new DateTime(2014, 6, 4), Latitude = 33.748995, Longitude = -84.387982, Order = 5 },
                    }
                };

                _worldContext.Trips.Add(usTrip1);
                _worldContext.Stops.AddRange(usTrip1.Stops);


                var usTrip2 = new Trip()
                {
                    Name = "Us Trip 2",
                    Created = DateTime.UtcNow,
                    UserName = "kimandersen",
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name = "Atlanta, Ga", Arrival = new DateTime(2014, 6, 4), Latitude = 33.748995, Longitude = -84.387982, Order = 0 }, 
                        new Stop() { Name = "Boston, MA", Arrival = new DateTime(2014, 6, 4), Latitude = 42.360082, Longitude = -71.058880, Order = 1 },
                        new Stop() { Name = "Seattle, WA", Arrival = new DateTime(2014, 6, 4), Latitude = 47.606209, Longitude = -122.332071, Order = 2 },
                        new Stop() { Name = "New York, NY", Arrival = new DateTime(2014, 6, 4), Latitude = 40.712784, Longitude = -74.005941, Order = 3 },
                        new Stop() { Name = "Chicago, IL", Arrival = new DateTime(2014, 6, 4), Latitude = 41.878114, Longitude = -87.629798, Order = 4 },     
                        new Stop() { Name = "Atlanta, Ga", Arrival = new DateTime(2014, 6, 4), Latitude = 33.748995, Longitude = -84.387982, Order = 5 },
                    }
                };

                _worldContext.Trips.Add(usTrip2);
                _worldContext.Stops.AddRange(usTrip2.Stops);

                var usTrip3 = new Trip()
                {
                    Name = "Us Trip 3",
                    Created = DateTime.UtcNow,
                    UserName = "kimandersen",
                    Stops = new List<Stop>()
                    {
                        new Stop() { Name = "Atlanta, Ga", Arrival = new DateTime(2014, 6, 4), Latitude = 33.748995, Longitude = -84.387982, Order = 0 },
                        new Stop() { Name = "New York, NY", Arrival = new DateTime(2014, 6, 4), Latitude = 40.712784, Longitude = -74.005941, Order = 1 },
                        new Stop() { Name = "Boston, MA", Arrival = new DateTime(2014, 6, 4), Latitude = 42.360082, Longitude = -71.058880, Order = 2 },
                        new Stop() { Name = "Seattle, WA", Arrival = new DateTime(2014, 6, 4), Latitude = 47.606209, Longitude = -122.332071, Order = 3 },
                        new Stop() { Name = "New York, NY", Arrival = new DateTime(2014, 6, 4), Latitude = 40.712784, Longitude = -74.005941, Order = 4 },
                        new Stop() { Name = "Boston, MA", Arrival = new DateTime(2014, 6, 4), Latitude = 42.360082, Longitude = -71.058880, Order = 5 },
                        new Stop() { Name = "Chicago, IL", Arrival = new DateTime(2014, 6, 4), Latitude = 41.878114, Longitude = -87.629798, Order = 6 },
                        new Stop() { Name = "Seattle, WA", Arrival = new DateTime(2014, 6, 4), Latitude = 47.606209, Longitude = -122.332071, Order = 7 },
                        new Stop() { Name = "New York, NY", Arrival = new DateTime(2014, 6, 4), Latitude = 40.712784, Longitude = -74.005941, Order = 8 },
                        new Stop() { Name = "Boston, MA", Arrival = new DateTime(2014, 6, 4), Latitude = 42.360082, Longitude = -71.058880, Order = 9 },
                        new Stop() { Name = "Atlanta, Ga", Arrival = new DateTime(2014, 6, 4), Latitude = 33.748995, Longitude = -84.387982, Order = 10 },
                    }
                };

                _worldContext.Trips.Add(usTrip3);
                _worldContext.Stops.AddRange(usTrip3.Stops);

                _worldContext.SaveChanges();
            }
        }
    }
}
