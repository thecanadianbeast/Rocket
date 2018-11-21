using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rocket.Models;
using System.Linq;
using System.Collections.Generic;


namespace Rocket.Controllers {
    [Route ("api/batteries")]
    [ApiController]
    public class BatteriesController : ControllerBase {
        private readonly MySql_appContext _context;

        public BatteriesController (MySql_appContext context) {
            _context = context;
        }

        // GET api/batteries/5
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
        // GET api/batteries/total
        [HttpGet ("total")]
        public ActionResult<List<Batteries>> GetAllBatteries () {
            var list2 = _context.Batteries.ToList ();
            if (list2 == null) {
                return NotFound ("Not Found");
            }
            List<Batteries> list_bat = new List<Batteries> ();

            foreach (var e in list2) {
                list_bat.Add (e);
            }
            return list_bat;
        }

        // PUT api/batteries/5
        [HttpPut ("{id}")]
        public ActionResult Update (long id, Batteries battery) {
            var bat = _context.Batteries.Find (id);
            if (bat == null) {
                return NotFound ();
            }

            bat.Status = battery.Status;

            _context.Batteries.Update (bat);
            _context.SaveChanges ();
            return NoContent ();
        }
    }
}