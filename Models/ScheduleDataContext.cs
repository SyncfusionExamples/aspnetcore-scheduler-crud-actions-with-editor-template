using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EJ2ScheduleSample.Models
{
    public class ScheduleDataContext:DbContext
    {
        public ScheduleDataContext(DbContextOptions<ScheduleDataContext> options)
           : base(options)
        { }

        public DbSet<ScheduleEvent> ScheduleEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScheduleEvent>().ToTable("ScheduleEvent");
        }
    }

    public class ScheduleEvent
    {        
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceID { get; set; }
        public string RecurrenceRule { get; set; }
        public string RecurrenceException { get; set; }
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }
    }
}
