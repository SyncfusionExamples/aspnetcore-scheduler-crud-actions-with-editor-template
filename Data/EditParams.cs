using EJ2ScheduleSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EJ2ScheduleSample.Data
{
    public class EditParams
    {
        public string key { get; set; }
        public string action { get; set; }
        public List<ScheduleEvent> added { get; set; }
        public List<ScheduleEvent> changed { get; set; }
        public List<ScheduleEvent> deleted { get; set; }
        public ScheduleEvent value { get; set; }
    }
}
