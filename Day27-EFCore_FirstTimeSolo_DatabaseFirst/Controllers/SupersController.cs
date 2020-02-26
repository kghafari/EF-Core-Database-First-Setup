using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Day27_EFCore_FirstTimeSolo_DatabaseFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day27_EFCore_FirstTimeSolo_DatabaseFirst.Controllers
{
    public class SupersController : Controller
    {
        private readonly SupersDbContext _context;

        public SupersController(SupersDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //print out all the supers
            return View(_context.Super.ToList());
        }

        public IActionResult ListMissionsBySuper(int id)
        {
            List<Mission> superMission = _context.Mission.Where(x => x.SuperId == id).ToList();
            return View(superMission);
        }

        public IActionResult ChangeStatus(int id)
        {
            //find the mission who's status we want to change
            Mission found = _context.Mission.Find(id);

            //make sure that mission is not null
            if(found != null)
            {
                //change/update stuff (flip the bool)
                found.Completed = !found.Completed;

                //modify the state of this entry IN THE DATABASE
                _context.Entry(found).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.Update(found);
                _context.SaveChanges();
            }

            //return RedirectToAction("ListMissionsBySuper", found.SuperId);
            List<Mission> superMission = _context.Mission.Where(x => x.SuperId == found.SuperId).ToList();
            return View("ListMissionsBySuper", superMission);
        }

       
    }
}