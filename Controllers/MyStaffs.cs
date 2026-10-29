using ADONET.Models;
using Microsoft.AspNetCore.Mvc;

namespace ADONET.Controllers
{
    public class MyStaffs : Controller
    {
        private readonly IStaff _staffService;

        public MyStaffs(IStaff staffService)
        {
            _staffService = staffService;
        }

        public async Task<IActionResult> Index()
        {
            var staff = await _staffService.Getallstaffs();
            return View("List", staff);
        }

        public async Task<IActionResult> GetbyId(int id)
        {
            var staff = await _staffService.Getstaffbyid(id);
            return View("Detail", staff);
        }

        public IActionResult Addstaff()
        {
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Add(Staffs staff)
        {
            await _staffService.AddStaff(staff);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _staffService.Getstaffbyid(id);
            return View(staff);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult>Remove(int id)
        {
            await _staffService.DeleteStaff(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult>Edit(int id)
        {
            var staff = await _staffService.Getstaffbyid(id);
            return View(staff);
        }
        [HttpPost]
        
        public async Task<IActionResult>Update(Staffs staff)
        {
             await _staffService.Updatestaff(staff);
            return RedirectToAction("Index");
        }
      
       
    }

}
