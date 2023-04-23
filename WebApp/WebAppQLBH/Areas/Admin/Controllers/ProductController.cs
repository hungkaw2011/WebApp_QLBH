using WebApp.DataAccess;
using WebApp.DataAccess.Repository.IRepository;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Admin.Controllers
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
            IEnumerable<Product> objCategoryList = _unitOfWork.Product.GetAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product productObj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productObj);
                _unitOfWork.Save();
                TempData["success"] = "Product Added Successfully";
                return RedirectToAction("Index");
            }
            return View(productObj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            // var categoryFromDb = _db.Categories.FirstOrDefault(u=>u.Id==id);
            // var categoryFromDb = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product productObj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(productObj);
                _unitOfWork.Save();
                TempData["success"] = "Product Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(productObj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // var categoryFromDb = _db.Categories.Find(id);
            var productFromDb = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            // var categoryFromDb = _db.Categories.SingleOrDefault(u=>u.Id==id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted Successfully";
            return RedirectToAction("Index");

        }
    }
}
