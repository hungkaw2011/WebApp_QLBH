using Microsoft.AspNetCore.Mvc;
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
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
