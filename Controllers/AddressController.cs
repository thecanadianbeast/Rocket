using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rocket.Models;

namespace Rocket.Controllers {

    // [Route ("api/elevators")]
    [Route ("api/city")]
    [ApiController]
    public class AddressesControllers : ControllerBase {
        private readonly MySql_appContext _context;

        public AddressesControllers (MySql_appContext context) {
            _context = context;
        }

       
        [HttpGet]
        public ActionResult<List<string>> GetAll () {

            var _result = _context.Addresses.Select (s => s.City).Distinct ();

            return _result.ToList ();


        }
    }
}