using Microsoft.AspNetCore.Mvc;
using WorkshopWebApi.Data;
using WorkshopWebApi.Models;

namespace WorkshopWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkshopController : ControllerBase
    {
        
        private readonly ILogger<WorkshopController> _logger;

        private readonly MyAppContext _context;

        public WorkshopController(ILogger<WorkshopController> logger, MyAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet()]
       
        public List<Workshop> Get()
        {
            _logger.LogInformation("Получаю список цехов");

            return _context.Workshop.ToList();
        }

        [HttpGet("{id}")]

        public Workshop? Get(int id)
        {
            return _context.Workshop
                .FirstOrDefault(x => x.Id == id);
        }

        [HttpPost()]
        public IEnumerable<Workshop> Post(string? nameWorkshop, string? groupWorkshop)
        {
            _logger.LogInformation("Получаю список цехов с фильтром");

            var query = _context.Workshop.AsQueryable();

            if (!string.IsNullOrEmpty(nameWorkshop))
            {
                query = query.Where(x => x.NameWorkshop.Contains(nameWorkshop));

            }

            if (!string.IsNullOrEmpty(groupWorkshop))
            {
                query = query.Where(x => x.GroupWorkshop.Contains(groupWorkshop));

            }

            var workshop = query.ToList();


            return workshop;
        }

        [HttpPut()]
        public Workshop Put(WorkshopAdd request)
        {
            _logger.LogInformation("Добавляю новый цех");

            var workshop = new Workshop
            {                
                NameWorkshop = request.NameWorkshop,
                GroupWorkshop = request.GroupWorkshop,
                EquipmentId = request.EquipmentId
            };

            _context.Workshop.Add(workshop);
            _context.SaveChanges();

            return workshop;
        }

        [HttpDelete()]
        public IActionResult Delete( int workshopId)
        { 
            var workshop = _context.Workshop.FirstOrDefault(x => x.Id == workshopId);

            if (workshop != null)
            {
                _context.Remove(workshop);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();
            
        }

        [HttpPatch()]
        public IActionResult Patch(Workshop model)
        {
            var workshop = _context.Workshop.FirstOrDefault(x => x.Id == model.Id);

            if (workshop == null)
            
                {
                    return NotFound();
                }

                workshop.NameWorkshop = model.NameWorkshop;
                workshop.GroupWorkshop = model.GroupWorkshop;

                _context.SaveChanges();

                return Ok();
            
            return NotFound();

        }

       
    }

}
