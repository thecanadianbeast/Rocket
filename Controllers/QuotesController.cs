using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket.Models;
using Newtonsoft.Json.Linq;



namespace Rocket.Controllers {

    [Route ("api/quotes")]
    [ApiController]
    public class QuotesController : ControllerBase {
        private readonly MySql_appContext _context;
        public QuotesController (MySql_appContext context) {
            _context = context;
        }

        [Route ("")]
        [HttpGet]
        public ActionResult<List<Quotes>> GetAll () {
            var list2 = _context.Quotes.ToList ();
            if (list2 == null) {
                return NotFound ("Not Found");
            }
            List<Quotes> list_alarm_inac = new List<Quotes> ();

            foreach (var e in list2) {
                list_alarm_inac.Add (e);
            }
            return list_alarm_inac;
        
        }

        [HttpGet("{id}")]
        public ActionResult<Quotes> Get(long id)
        {
            var item = _context.Quotes.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            var res = new JObject();
            res["id"] = item.Id;
            res["QuoteType"] = item.QuoteType;
            res["BusinessName"] = item.BusinessName;
            res["Email"] = item.Email;
            return Content(res.ToString(), "application/json");
        }
    }
}