using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WorkshopClientApp.Data;
using WorkshopClientApp.Models;
using WorkshopClientApp.Services;

namespace WorkshopClientApp.Controllers
{
[Authorize]
public class HomeController : Controller
    {        
        private readonly ILogger<HomeController> _logger;

        private readonly WorkshopClientService _workshopClientService;

        private readonly EquipmentClientService _equipmentClientService;

        public HomeController(ILogger<HomeController> logger, 
            WorkshopClientService workshopClientService, 
            EquipmentClientService equipmentClientService)
        {
            _logger = logger;
            _workshopClientService = workshopClientService;
            _equipmentClientService = equipmentClientService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string nameWorkshop, string groupWorkshop)
        {
            var responseData = await _workshopClientService.SearchWorkshops(nameWorkshop, groupWorkshop);

            return View(responseData);
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var worksopData = await _workshopClientService.GetWorkshopAll();

            return View(worksopData);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WorkshopAddDto request)
        {
            await _workshopClientService.AddWorkshop(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var equipments = await _workshopClientService.GetEquipmentAll();

            return View(equipments);
        }

        #region -- Редактирование

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WorkshopEditDto request)
        {
            if (request.Id <= 0)
            {
                return BadRequest("Не передан корректный Id цеха.");
            }

            await _workshopClientService.Edit(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var responseData = await _workshopClientService.GetWorkshopById(id);

            return View(responseData);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {

            await _workshopClientService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        //Методы Equipment (Оборудование)

        [HttpPost]
        public async Task<IActionResult> IndexEquipment(string name)
        {
            var responseData = await _equipmentClientService.SearchEquipments(name);

            return View(responseData);
        }

        [HttpGet]
        public async Task<IActionResult> IndexEquipment()
        {
            var equipmentData = await _equipmentClientService.GetEquipmentAll();

            return View(equipmentData);
        }

        [HttpPost]
        public async Task<IActionResult> AddEquipment(EquipmentAddDto request)
        {
            await _equipmentClientService.AddEquipment(request);

            return RedirectToAction(nameof(IndexEquipment));
        }

        [HttpGet]
        public async Task<IActionResult> AddEquipment()
        {
            var equipments = await _equipmentClientService.GetEquipmentAll();

            return View(equipments);
        }

        #region -- Редактирование

        [HttpPost]
        public async Task<IActionResult> EditEquipment(EquipmentEditDto request)
        {
            await _equipmentClientService.EditEquipment(request);

            return RedirectToAction(nameof(IndexEquipment));
        }

        [HttpGet]
        public async Task<IActionResult> EditEquipment(int id)
        {
            var responseData = await _equipmentClientService.GetEquipmentById(id);

            return View(responseData);
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> RemoveEquipment(int id)
        {

            await _equipmentClientService.RemoveEquipment(id);

            return RedirectToAction(nameof(IndexEquipment));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
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
