using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EJ2ScheduleSample.Models;
using EJ2ScheduleSample.Data;
using Newtonsoft.Json;

namespace EJ2ScheduleSample.Controllers
{
    public class HomeController : Controller
    {
        private ScheduleDataContext _context;
        public IActionResult Index()
        {

            List<ResourceDataSourceModel> projects = new List<ResourceDataSourceModel>();
            projects.Add(new ResourceDataSourceModel { text = "PROJECT 1", id = 1, color = "#cb6bb2" });
            projects.Add(new ResourceDataSourceModel { text = "PROJECT 2", id = 2, color = "#56ca85" });
            projects.Add(new ResourceDataSourceModel { text = "PROJECT 3", id = 3, color = "#df5286" });
            ViewBag.Projects = projects;

            List<ResourceDataSourceModel> categories = new List<ResourceDataSourceModel>();
            categories.Add(new ResourceDataSourceModel { text = "Nancy", id = 1, groupId = 1, color = "#df5286" });
            categories.Add(new ResourceDataSourceModel { text = "Steven", id = 2, groupId = 1, color = "#7fa900" });
            categories.Add(new ResourceDataSourceModel { text = "Robert", id = 3, groupId = 2, color = "#ea7a57" });
            categories.Add(new ResourceDataSourceModel { text = "Smith", id = 4, groupId = 2, color = "#5978ee" });
            categories.Add(new ResourceDataSourceModel { text = "Micheal", id = 5, groupId = 3, color = "#df5286" });
            categories.Add(new ResourceDataSourceModel { text = "Root", id = 6, groupId = 3, color = "#00bdae" });
            categories.Add(new ResourceDataSourceModel { text = "RK", id = 7, groupId = 4, color = "#5978ee" });
            categories.Add(new ResourceDataSourceModel { text = "Dk", id = 8, groupId = 5, color = "#df5286" });
            categories.Add(new ResourceDataSourceModel { text = "Stev", id = 9, groupId = 6, color = "#df5286" });
            ViewBag.Categories = categories;

            List<ResourceDataSourceModel> projectsList = new List<ResourceDataSourceModel>();
            projectsList.Add(new ResourceDataSourceModel { text = "PROJECT 4", id = 4, color = "#bbdc00" });
            projectsList.Add(new ResourceDataSourceModel { text = "PROJECT 5", id = 5, color = "#9e5fff" });
            ViewBag.ProjectsList = projectsList;

            List<ResourceDataSourceModel> personsList = new List<ResourceDataSourceModel>();
            personsList.Add(new ResourceDataSourceModel { text = "Alice", id = 11, groupId = 4, color = "#bbdc00" });
            personsList.Add(new ResourceDataSourceModel { text = "Nancy", id = 12, groupId = 5, color = "#9e5fff"});
            personsList.Add(new ResourceDataSourceModel { text = "Robert", id = 13, groupId = 5, color = "#bbdc00"});
            personsList.Add(new ResourceDataSourceModel { text = "Robson", id = 14, groupId = 4, color = "#9e5fff"});
            personsList.Add(new ResourceDataSourceModel { text = "Laura", id = 15, groupId = 4, color = "#bbdc00" });
            personsList.Add(new ResourceDataSourceModel { text = "Margaret", id = 16, groupId = 5, color = "#9e5fff"});
            ViewBag.PersonsList = personsList;            

            ViewBag.Resources = new string[] { "Projects", "Categories" };
            return View();
        }
        public IActionResult Schedule()
        {
            return View();
        }
        class ResourceDataSourceModel
        {
            public int id { set; get; }
            public string text { set; get; }
            public string color { set; get; }
            public int? groupId { set; get; }
        }


        [HttpPost]
        public List<ScheduleEvent> LoadData([FromBody]Params param)
        {
            DateTime start = (param.CustomStart != new DateTime()) ? param.CustomStart : param.StartDate;
            DateTime end = (param.CustomEnd != new DateTime()) ? param.CustomEnd : param.EndDate;
            return _context.ScheduleEvents.Where(app => (app.StartTime >= start && app.StartTime <= end) || (app.RecurrenceRule != null && app.RecurrenceRule != "")).ToList(); // Here filtering the events based on the start and end date value.
        }

        public class Params
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public DateTime CustomStart { get; set; }
            public DateTime CustomEnd { get; set; }
        }

        public class EditParams
        {
            public string key { get; set; }
            public string action { get; set; }
            public List<ScheduleEvent> added { get; set; }
            public List<ScheduleEvent> changed { get; set; }
            public List<ScheduleEvent> deleted { get; set; }
            public ScheduleEvent value { get; set; }
        }

        [HttpPost]
        public List<ScheduleEvent> UpdateData([FromBody]EditParams param)
        {
            if (param.action == "insert" || (param.action == "batch" && param.added.Count > 0)) // this block of code will execute while inserting the appointments
            {
                int intMax = _context.ScheduleEvents.ToList().Count > 0 ? _context.ScheduleEvents.ToList().Max(p => p.Id) : 1;
                for (var i = 0; i < param.added.Count; i++)
                {
                    var value = (param.action == "insert") ? param.value : param.added[i];
                    DateTime startTime = Convert.ToDateTime(value.StartTime);
                    DateTime endTime = Convert.ToDateTime(value.EndTime);
                    ScheduleEvent appointment = new ScheduleEvent()
                    {
                        StartTime = startTime,
                        EndTime = endTime,
                        Subject = value.Subject,
                        IsAllDay = value.IsAllDay,
                        StartTimezone = value.StartTimezone,
                        EndTimezone = value.EndTimezone,
                        RecurrenceRule = value.RecurrenceRule,
                        RecurrenceID = value.RecurrenceID,
                        RecurrenceException = value.RecurrenceException,
                        Description = value.Description,
                        CategoryId = value.CategoryId,
                        ProjectId = value.ProjectId
                    };
                    _context.ScheduleEvents.Add(appointment);
                    _context.SaveChanges();
                }           
            }
            if (param.action == "update" || (param.action == "batch" && param.changed.Count > 0)) // this block of code will execute while removing the appointment
            {
                var value = (param.action == "update") ? param.value : param.changed[0];
                var filterData = _context.ScheduleEvents.Where(c => c.Id == Convert.ToInt32(value.Id));
                if (filterData.Count() > 0)
                {
                    DateTime startTime = Convert.ToDateTime(value.StartTime);
                    DateTime endTime = Convert.ToDateTime(value.EndTime);
                    ScheduleEvent appointment = _context.ScheduleEvents.Single(A => A.Id == Convert.ToInt32(value.Id));
                    appointment.StartTime = startTime;
                    appointment.EndTime = endTime;
                    appointment.StartTimezone = value.StartTimezone;
                    appointment.EndTimezone = value.EndTimezone;
                    appointment.Subject = value.Subject;
                    appointment.IsAllDay = value.IsAllDay;
                    appointment.RecurrenceRule = value.RecurrenceRule;
                    appointment.RecurrenceID = value.RecurrenceID;
                    appointment.RecurrenceException = value.RecurrenceException;
                    appointment.Description = value.Description;
                    appointment.ProjectId = value.ProjectId;
                    appointment.CategoryId = value.CategoryId;
                }
                _context.SaveChanges();
            }
            if (param.action == "remove" || (param.action == "batch" && param.deleted.Count > 0)) // this block of code will execute while updating the appointment
            {
                if (param.action == "remove")
                {
                    int key = Convert.ToInt32(param.key);
                    ScheduleEvent appointment = _context.ScheduleEvents.Where(c => c.Id == key).FirstOrDefault();
                    if (appointment != null) _context.ScheduleEvents.Remove(appointment);
                }
                else
                {
                    foreach (var apps in param.deleted)
                    {
                        ScheduleEvent appointment = _context.ScheduleEvents.Where(c => c.Id == apps.Id).FirstOrDefault();
                        if (apps != null) _context.ScheduleEvents.Remove(appointment);
                    }
                }
                _context.SaveChanges();
            }
            return _context.ScheduleEvents.ToList();
        }


        public HomeController(ScheduleDataContext context)
        {
            _context = context;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
