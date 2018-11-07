using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket.Models;

namespace Rocket.Controllers {
    [Route ("api/users")]
    [ApiController]
    public class EmployeesController : ControllerBase {
        private readonly MySql_appContext _context;
        public EmployeesController (MySql_appContext context) {
            _context = context;
        }

        // GET api/users
        [HttpGet]
        public ActionResult<List<Employees>> GetAll () {
            var listl = _context.Employees;

            List<Employees> list_employees = new List<Employees> ();
            foreach (var e in listl) {
                list_employees.Add (e);
            }
            return list_employees;
        }
    }
}
}
}