using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rocket.Models;

namespace Rocket.Controllers {
    [Route ("api/batteries")]
    [ApiController]
    public class BatteriesController : ControllerBase {
        private readonly David_appContext _context;

        public BatteriesController (David_appContext context) {
            _context = context;
        }

        // GET api/batteries
        [HttpGet ("{id}")]
        public ActionResult GetById (string Status, long id) {
            var item = _context.Batteries.Find (id);
            if (item == null) {
                return NotFound ("Not found");
            }
            var json = new JObject ();
            json["status"] = item.Status;
            return Content (json.ToString (), "application/json");
        }

        // // GET api/batteries/5
        // [HttpGet ("{id}")]
        // public ActionResult<string> Get (int id) {
        //     return "value";
        // }

        // PUT api/batteries/5
        [HttpPut ("{id}")]
        public ActionResult Update (long id, Batteries battery) {
            var bat  = _context.Batteries.Find (id);
            if (bat == null) {
                return NotFound ();
            }

            // bat.isComplete = status.isComplete;
            bat.Status = battery.Status;

            _context.Batteries.Update (bat);
            _context.SaveChanges ();
            return NoContent ();
        }

    }
}
