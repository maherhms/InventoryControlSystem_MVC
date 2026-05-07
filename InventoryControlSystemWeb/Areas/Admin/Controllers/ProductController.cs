using AspNetCoreGeneratedDocument;
using InventoryControlSystem.DataAccess.Data;
using InventoryControlSystem.DataAccess.Repository.IRepository;
using InventoryControlSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InventoryControlSystemWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            return View(objProductList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });

            //ViewBag.CategoryList = CategoryList;
            ViewData["CategoryList"] = CategoryList;

            return View();
        }
        [HttpPost , ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePOST(Product entity)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(entity);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index","Product");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost , ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPOST(Product entity)
        {
            if (ModelState.IsValid)
            {
                Product? product = _unitOfWork.Product.Get(u => u.Id == entity.Id);
                if (product == null)
                {
                    return NotFound();
                }

                product.Title = entity.Title;
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index", "Product");
            }
            return View(entity);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int id)
        {
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index", "Product");
        }
    }
}
