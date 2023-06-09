﻿using WebApp.DataAccess;
using WebApp.DataAccess.Repository.IRepository;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models.ViewModels;
using NuGet.Protocol.Plugins;
using Microsoft.Extensions.Hosting;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //lưu trữ thư mục chứa tài nguyên như wwwroot, appsettings.json, các biến môi trường và các thông tin khác như địa chỉ IP, port...

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        #region CRUD
        //GET
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                //Create
                return View(new Company());
            }
            else
            {
                //Update
                Company company=_unitOfWork.Company.GetFirstOrDefault(u=>u.Id == id);
                return View(company);
            }
        }
        /// <summary>
        ///     String fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
        ///     Tạo ra một chuỗi định danh duy nhất được sử dụng làm tên file
        //      Ví dụ: Nếu file.FileName là "product_image.jpg", Guid.NewGuid().ToString()+Path.GetExtension(file.FileName)
        //      Có thể trả về chuỗi "f8dd1a1b-6d9a-4f67-a8a8-246c2ee0d97a.jpg", là tên file mới được tạo ra để lưu trữ hình ảnh sản phẩm.
        /// </summary>
        /// <param name="productObj"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)//Kiểm tra tính hợp lệ của dữ liệu 
            {
                
                if (companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);
                    _unitOfWork.Save();
                    TempData["success"] = "Company Added Successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);
                    _unitOfWork.Save();
                    TempData["success"] = "Company Updated Successfully";
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }
        }
        #endregion
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() { 
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data= objCompanyList });
        }
        [HttpDelete]
		public IActionResult Delete(int? id)
		{
			var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
			if (obj == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			} 
			_unitOfWork.Company.Remove(obj);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Deleted Successfully" });
		}
		#endregion
	}
}
