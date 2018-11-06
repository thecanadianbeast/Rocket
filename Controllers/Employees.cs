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


        // GET api/users/list
        [HttpGet]
        public ActionResult<List<Employees>> GetAll () {
            List<Employees> list_email = new List<Employees> ();

            foreach (var e in list_email) {

                if (e.Email == "") {
                    list_email.Add (e);
                }
            }
            return list_email;
        }

    }
}