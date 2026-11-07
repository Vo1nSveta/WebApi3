using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp1_T1.Data;
using WebApp1_T1.Models;

namespace WebApp1_T1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MyAppContext _context;

        public HomeController(ILogger<HomeController> logger, MyAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult Index(string workshop, string equipment)
        {
            var query = _context.Workshop.AsQueryable();

            if(!string.IsNullOrEmpty(workshop))
            {
                query = query.Where(x => x.workshop.Contains(workshop));

            }

            if (!string.IsNullOrEmpty(equipment))
            {
                query = query.Where(x => x.equipment.Contains(equipment));

            }

            var Workshop = query.ToList();

            return View(Workshop);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var workshop = _context.Workshop.ToList();

            return View(workshop);
        }

        [HttpPost]
        public IActionResult Add(string workshop, string equipment)
        {
            if (string.IsNullOrWhiteSpace(workshop) ||
                string.IsNullOrWhiteSpace(equipment))
            {
                ModelState.AddModelError("", "Заполните оба поля: цех и оборудование.");
                return View();
            }

            var newWorkshop = new Workshop
            {
                workshop = workshop,
                equipment = equipment
            };

            _context.Workshop.Add(newWorkshop);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        #region -- Редактирование

        [HttpPost]
        public IActionResult Edit(string workshop, string equipment, int id)
        {
            var newWorkshop = _context.Workshop.FirstOrDefault(x => x.Id == id);

            if (newWorkshop != null)
            {
                newWorkshop.workshop = workshop;
                newWorkshop.equipment = equipment;

                _context.Workshop.Update(newWorkshop);
                _context.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var newWorkshop = _context.Workshop.FirstOrDefault(x => x.Id == id);

            return View(newWorkshop);
        }

        #endregion


        [HttpGet]
        public IActionResult Add()
        {
      
            return View();
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var newWorkshop = _context.Workshop.FirstOrDefault(x => x.Id == id);

            if (newWorkshop != null)
            {

                _context.Workshop.Remove(newWorkshop);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
