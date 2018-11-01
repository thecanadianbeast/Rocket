using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Rocket.Models;

namespace Rocket.Controllers {

    [Route ("api/buildings")]
    [ApiController]
    public class BuildingsControllers : ControllerBase {
        private readonly MySql_appContext _context;

        public BuildingsControllers (MySql_appContext context) {
            _context = context;
        }

        // GET api/buildings
        [HttpGet]
        public ActionResult<List<Buildings>> GetAll () {
            var list = _context.Buildings
                .Include (bu => bu.Batteries)
                .ThenInclude (b => b.Columns)
                .ThenInclude (c => c.Elevators);

            if (list == null) {
                return NotFound ("Not Found");
            }

            // To build a "Buildings" list
            List<Buildings> list_buildings_intervention = new List<Buildings> ();
            var verification = false;

            foreach (var building in list) {
                verification = false;
                foreach (var battery in building.Batteries) {
                    if (battery.Status == "Intervention") {
                        verification = true;
                    }
                    foreach (var column in battery.Columns) {
                        if (column.Status == "Intervention") {
                            verification = true;
                        }

                        foreach (var elevator in column.Elevators) {
                            if (elevator.Status == "Intervention") {
                                verification = true;
                            }
                        }
                    }
                }
                if (verification == true) {
                    var found_building = _context.Buildings.Find (building.Id);
                    list_buildings_intervention.Add (found_building);
                }
            }

            // To build a "json object" list of buildings
            // var jsons = new List<JObject> ();
            // foreach (var b in list_buildings_intervention) {
            //     var json = new JObject ();
            //     json["id"] = b.Id;
            //     json["business name"] = _context.Customers.Find (b.CustomerId).BusinessName;
            //     json["building name"] = b.BuildingName;
            //     json["building address"] = $"{_context.Addresses.Find(b.AddressId).StreetAddress}, {_context.Addresses.Find(b.AddressId).City} {_context.Addresses.Find(b.AddressId).State}  {_context.Addresses.Find(b.AddressId).ZipCode}";
            //     json["administrator name"] = b.AdministratorFullName;
            //     json["administrator phone"] = b.AdministratorPhone;
            //     json["administrator email"] = b.AdministratorEmail;
            //     json["technician name"] = b.TechnicianFullName;
            //     json["technician phone"] = b.TechnicianPhone;
            //     json["technician email"] = b.TechnicianEmail;
            //     jsons.Add (json);
            // }
            return list_buildings_intervention;
        }
    }
}