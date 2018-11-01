using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rocket.Models;

namespace Rocket.Controllers {

    [Route ("api/leads")]
    [ApiController]
    public class LeadsController : ControllerBase {
        
        private readonly David_appContext _context;

        public LeadsController (David_appContext context) {
            _context = context;
        }


        [HttpGet]
        public ActionResult<List<Leads>> GetAll () {
            var listl = _context.Leads.ToList ();
            var listc = _context.Customers.ToList ();
            // var duration = new System.TimeSpan(-30, 0, 0, 0);
            // var month = DateTime.Now.Add(duration);
            if (listl == null) {
                return NotFound ("Not Found");
            }
            if (listc == null) {
                return NotFound ("Not Found");
            }

            List<Leads> list_lead = new List<Leads> ();
            DateTime currentDate = DateTime.Now.AddDays(-30);

            foreach (var l in listl) {

                if (l.CreatedAt >= currentDate ) {
                    list_lead.Add (l);
                }
            }
            return list_lead;
        }

    }
}