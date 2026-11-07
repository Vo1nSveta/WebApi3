using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopWebApi.Data;
using WorkshopWebApi.Models;

namespace WorkshopWebApi.Controllers

{ 
    [ApiController]
    [Route("[controller]")]

    public class EquipmentController : ControllerBase
    {
        private readonly MyAppContext _context;

        public EquipmentController(ILogger<WorkshopController> logger, MyAppContext context)
        {
            _context = context;
        }

    [HttpGet]

        public List<Equipment> Get()
        {
            
            return _context.Equipment
            .ToList();
        }

        [HttpGet("{id}")]

        public Equipment? Get(int id)
        {
            return _context.Equipment
                .FirstOrDefault(x => x.Id == id);
        }

        [HttpPost()]
        public IEnumerable<Equipment> Post(string? Name)
        {           
            var query = _context.Equipment.AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(x => x.Name.Contains(Name));

            }
                       
            var equipment = query.ToList();


            return equipment;
        }

        [HttpPut()]
        public Equipment Put(EquipmentAdd request)
        {
            var equipment = new Equipment
            {
                Name = request.Name,
            };

            _context.Equipment.Add(equipment);
            _context.SaveChanges();

            return equipment;
        }

        [HttpDelete()]
        public IActionResult Delete(int equipmentId)
        {
            var equipment = _context.Equipment.FirstOrDefault(x => x.Id == equipmentId);

            if (equipment != null)
            {
                _context.Remove(equipment);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();

        }

        [HttpPatch()]
        public IActionResult Patch(Equipment model)
        {
            var equipment = _context.Equipment.FirstOrDefault(x => x.Id == model.Id);

            if (equipment != null)
            {
                equipment.Name = model.Name;
               
                _context.Update(equipment);
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();

        }
    }
}
