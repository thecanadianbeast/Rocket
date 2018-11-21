using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Rocket.Models;
using System.Linq;


namespace Rocket.Controllers {

    [Route ("api/customers")]
    [ApiController]
    public class CustomersControllers : ControllerBase {
        private readonly MySql_appContext _context;

        public CustomersControllers (MySql_appContext context) {
            _context = context;
        }

        [Route("total")]
        [HttpGet]
        public ActionResult<List<Customers>> GetAllCustomers () {
            var list2 = _context.Customers.ToList();
            if (list2 == null) {
                return NotFound ("Not Found");
            }
            List<Customers> list_customers = new List<Customers> ();

            foreach (var e in list2) {
                list_customers.Add(e);
            }
            return list_customers;
        }
    }
}
