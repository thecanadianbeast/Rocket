using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Rocket.Models;
using System.Linq;


namespace Rocket.Controllers {

    [Route ("api/buildings")]
    [ApiController]
    public class BuildingsControllers : ControllerBase {
        private readonly MySql_appContext _context;

        public BuildingsControllers (MySql_appContext context) {
            _context = context;
        }

        [Route("total")]
        [HttpGet]
        public ActionResult<List<Buildings>> GetAllBuildings () {
            var list2 = _context.Buildings.ToList();
            if (list2 == null) {
                return NotFound ("Not Found");
            }
            List<Buildings> list_buildings = new List<Buildings> ();

            foreach (var e in list2) {
                list_buildings.Add (e);
            }
            return list_buildings;
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
            return list_buildings_intervention;
        }
    }
}