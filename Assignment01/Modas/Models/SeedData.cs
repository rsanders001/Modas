using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Modas.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            List<Location> Locations = new List<Location>();

            if (!context.Locations.Any())
            {
                Locations.Add(new Location { Name = "Family Room" });
                Locations.Add(new Location { Name = "Front Door" });
                Locations.Add(new Location { Name = "Rear Door" });
                foreach (var l in Locations)
                {
                    
                    context.Locations.Add(l);
                    context.SaveChanges();

                }
                
            }
            if (!context.Events.Any())
            {
                DateTime localDate = DateTime.Now;
                DateTime eventDate = localDate.AddMonths(-6);
                Random rnd = new Random();
                while (eventDate < localDate)
                {
                    int num = rnd.Next(0, 6);
                    SortedList<DateTime, Event> dailyEvents = new SortedList<DateTime, Event>();
                    for (int i = 0; i < num; i++)
                    {
                        int hour = rnd.Next(0, 24);
                        int minute = rnd.Next(0, 60);
                        int second = rnd.Next(0, 60);
                        int loc = rnd.Next(0, Locations.Count());
                        DateTime x = new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, hour, minute, second);
                        Event e = new Event { TimeStamp = x, Flagged = false, Location = context.Locations.FirstOrDefault(l => l.Name == Locations[loc].Name) };
                        dailyEvents.Add(e.TimeStamp, e);
                    }
                    foreach (var item in dailyEvents)
                    {
                        context.Events.Add(item.Value);
                        context.SaveChanges();
                    }
                    eventDate = eventDate.AddDays(1);
                }
            }
        }
    }
}