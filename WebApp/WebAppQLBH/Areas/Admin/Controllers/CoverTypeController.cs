using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using WebApp.Models;
using WebApp.DataAccess;
using WebApp.DataAccess.Repository.IRepository;
namespace WebAppQLBH.Areas.Admin.Controllers;
[Area("Admin")]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _db;

    public CoverTypeController(IUnitOfWork db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        var objCoverTypeList = _db.CoverType.GetAll();
        return View(objCoverTypeList);
    }
    //Get
    public IActionResult Create()
    {
        return View();
    }
    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        //bool containsNumber = Regex.IsMatch(category.Name, @"\d");
        //if (containsNumber)
        //{
        //    ModelState.AddModelError("Name", "Tên không được chứa chữ số!!!");
        //}
        if (ModelState.IsValid)
        {
            _db.CoverType.Add(obj);
            _db.Save();
            TempData["success"] = "CoverType created successfully";//lưu trữ tạm thời trên server, để lưu giữ thông tin giữa các Action hoặc Controller 
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    //Get
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var query = _db.CoverType.GetFirstOrDefault(i => i.Id == id);
        if (query == null)
        {
            return BadRequest();
        }
        return View(query);
    }

    //Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        //bool containsNumber = Regex.IsMatch(category.Name, @"\d");
        //if (containsNumber)
        //{
        //    ModelState.AddModelError("Name", "Tên không được chứa chữ số!!!");
        //}
        if (ModelState.IsValid)
        {
            _db.CoverType.Update(obj);
            _db.Save();
            TempData["success"] = "CoverType updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var query = _db.CoverType.GetFirstOrDefault(i => i.Id == id);
        if (query != null)
        {
            _db.CoverType.Remove(query);
            _db.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");
        }
        return View(query);
    }
}
