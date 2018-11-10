using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rocket.Models;

namespace Rocket.Controllers {

    [Route ("api/elevators")]
    [ApiController]
    public class ElevatorsControllers : ControllerBase {
        private readonly MySql_appContext _context;

        public ElevatorsControllers (MySql_appContext context) {
            _context = context;
        }

        // GET api/elevators/5
        [HttpGet ("{id}", Name = "GetElevators")]
        public string GetById (long id) {
            var item = _context.Elevators.Find (id);
            var status = item.Status;
            if (item == null) {
                return "Not Found";
            }
            
            return status;
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

        // PUT api/elevators/5
        [HttpPut ("{id}")]
        public ActionResult Update (long id, Elevators elevator) {
            var elv = _context.Elevators.Find (id);
            if (elv == null) {
                return NotFound ();
            }

            elv.Status = elevator.Status;

            _context.Elevators.Update (elv);
            _context.SaveChanges ();
            return NoContent ();
        }
    }
}