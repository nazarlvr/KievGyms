using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KievGyms.Models;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;

namespace KievGyms.Controllers
{
    public class GoogleMapsController : Microsoft.AspNetCore.Mvc.Controller
    {
        public GymDBContext _context = new GymDBContext();

        public GoogleMapsController(GymDBContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            //var gyms = new Object[]{ new { GymId = 10, GymName = "Test", DistrictId = 1, GymInfo = "Info", GeoLong = 50.3, GeoLat = 30.5 } };

            var gyms = _context.Gyms.ToList();
            return Json(gyms);
        }
    }
}