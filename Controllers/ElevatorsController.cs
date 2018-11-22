using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rocket.Models;

namespace Rocket.Controllers {

 
    // [Route ("api/elevators")]
    [Route("api/elevators")]
    [ApiController]
    public class ElevatorsControllers : ControllerBase {
        private readonly MySql_appContext _context;

        public ElevatorsControllers (MySql_appContext context) {
            _context = context;
        }

        // ------------------------- APP_MOBILE ------------------------- //
        // // GET api/elevators/5 // ------------------------- APP_MOBILE
        // [Route("{id}")]
        // [HttpGet]
        // public string GetById (long id) {
        //     var item = _context.Elevators.Find (id);
        //     var status = item.Status;
        //     if (item == null) {
        //         return "Not Found";
        //     }

        //     return status;
        // }
        // ------------------------- APP_MOBILE ------------------------- //

        [HttpGet("{id}")]
        public ActionResult<Elevators> Get(long id)
        {
            var item = _context.Elevators.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            var res = new JObject();
            res["id"] = item.Id;
            res["status"] = item.Status;
            return Content(res.ToString(), "application/json");
        }

        // GET api/elevators/list
        [Route("")]
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

        // GET api/elevators/total
        [Route("total")]
        [HttpGet]
        public ActionResult<List<Elevators>> GetAllElevator () {
            var list2 = _context.Elevators.ToList ();
            if (list2 == null) {
                return NotFound ("Not Found");
            }
            List<Elevators> list_alarm_inac = new List<Elevators> ();

            foreach (var e in list2) {
                list_alarm_inac.Add (e);
            }
            return list_alarm_inac;
        }

        // PUT api/elevators/5
        [Route("{id}")]
        [HttpPut]
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