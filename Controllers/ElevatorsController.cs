using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rocket.Models;

namespace Rocket.Controllers {

    [Route ("api/Elevator")]
    [ApiController]
    public class ElevatorControllers : ControllerBase {
        private readonly David_appContext _context;

        public ElevatorControllers (David_appContext context) {
            _context = context;
        }

        [HttpGet ("{id}", Name = "GetElevators")]
        public ActionResult GetById (string Status, long id) {
            var item = _context.Elevators.Find (id);
            if (item == null) {
                return NotFound ("Not Found");
            }
            var json = new JObject ();
            json["status"] = item.Status;
            return Content (json.ToString (), "application/json");
        }

        [HttpGet]
        public ActionResult<List<Elevators>> GetAll () {
            var list = _context.Elevators.ToList ();
            if (list == null) {
                return NotFound ("Not Found");
            }
            List<Elevators> list_alarm_inac = new List<Elevators> ();

            foreach (var e in list) {

                if (e.Status == "Alarm" || e.Status == "Inactive") {
                    list_alarm_inac.Add(e);
                }
            }
            return list_alarm_inac;
        }

        // PUT api/batteries/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }
    }
}