using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EJ2ScheduleSample.Models;

namespace EJ2ScheduleSample.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ScheduleDataContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.ScheduleEvents.Any())
            {
                return;   // DB has been seeded
            }

            var appointments = new ScheduleEvent[]
            {
            new ScheduleEvent{Id=1, Subject="Meeting", StartTime= new DateTime(2018,3,27,10,00,00), EndTime=new DateTime(2018,3,27,11,00,00), StartTimezone="", EndTimezone="", IsAllDay=false, Description="",Location="", RecurrenceID= "10", RecurrenceRule= "FREQ=DAILY;INTERVAL=1;COUNT=5"},
            new ScheduleEvent{Id=2, Subject="Client Demo", StartTime= new DateTime(2018,3,28,10,00,00), EndTime=new DateTime(2018,3,28,11,00,00), StartTimezone="", EndTimezone="", IsAllDay=false, Description="",Location=""},

            };
            foreach (ScheduleEvent app in appointments)
            {
                context.ScheduleEvents.Add(app);
            }
            context.SaveChanges();
        }
    }
}
