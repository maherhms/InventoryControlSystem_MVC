using InventoryControlSystemWeb.Data;
using InventoryControlSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControlSystemWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category entity)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(entity);
                _db.SaveChanges();
                return RedirectToAction("Index","Category");
            }
            return View();
        }
    }
}
