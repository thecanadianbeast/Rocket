using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Rocket.Models;

namespace Rocket.Controllers {
    [Route ("api/users")]
    [ApiController]

    public class EmployeesControllers : ControllerBase {
        private readonly MySql_appContext _context;

        public EmployeesControllers (MySql_appContext context) {
            _context = context;
        }

       // GET api/users/email
        [HttpGet ("{Email}")]
        public ActionResult GetById (string Email) {
            var item = _context.Employees.Find (Email);
            if (item == null) {
                return NotFound ("Not found");
            }
            var json = new JObject ();
            json["status"] = item.Email;
            return Content (json.ToString (), "application/json");
        }
    }

}