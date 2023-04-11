using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using WebAppQLBH.Data;
using WebAppQLBH.Models;

namespace WebAppQLBH.Controllers
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
            var objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            bool containsNumber = Regex.IsMatch(category.Name, @"\d");
            if (containsNumber)
            {
                ModelState.AddModelError("Name", "Tên không được chứa chữ số!!!");
            }
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Tên không được trùng lặp với số lượng đặt!!!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";//lưu trữ tạm thời trên server, để lưu giữ thông tin giữa các Action hoặc Controller 
                return RedirectToAction("Index");
            }
            return View(category);
        }
        //Get
        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return BadRequest();
            }
            return View(category);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            bool containsNumber = Regex.IsMatch(category.Name, @"\d");
            if (containsNumber)
            {
                ModelState.AddModelError("Name", "Tên không được chứa chữ số!!!");
            }
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Tên không được trùng lặp với số lượng đặt!!!");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category != null) {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
