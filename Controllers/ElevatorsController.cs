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

        // GET api/elevators/5
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

        // GET api/elevators/list
        [HttpGet]
        public ActionResult<List<Elevators>> GetAll () {
            var list = _context.Elevators.ToList ();
            if (list == null) {
                return NotFound ("Not Found");
            }
            List<Elevators> list_alarm_inac = new List<Elevators> ();

            foreach (var e in list) {

                if (e.Status == "Alarm" || e.Status == "Inactive") {
                    list_alarm_inac.Add (e);
                }
            }
            return list_alarm_inac;
        }

        // // PUT api/elevators
        // [HttpPut ("{id}")]
        // public ActionResult Update (long id, Elevators status) {
        //     var elv = _context.Elevators.Find (id);
        //     if (elv == null) {
        //         return NotFound ();
        //     }
        //     _context.Elevators.Update (bat);
        //     _context.SaveChanges ();
        //     return NoContent ();
        // }
    }
}