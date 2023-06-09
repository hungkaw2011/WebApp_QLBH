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
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IWebHostEnvironment _webHostEnvironment; 
        //lưu trữ thư mục chứa tài nguyên như wwwroot, appsettings.json, các biến môi trường và các thông tin khác như địa chỉ IP, port...

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment; 

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
#pragma warning disable IDE0090 // Use 'new(...)'
            ProductVM productVM = new ProductVM()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //Update
                productVM.Product=_unitOfWork.Product.GetFirstOrDefault(u=>u.Id == id);
                return View(productVM);
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
        public IActionResult Upsert(ProductVM productObj,IFormFile? file)
        {
            if (ModelState.IsValid)//Kiểm tra tính hợp lệ của dữ liệu 
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;//Lấy đường dẫn đến thư mục gốc của website.
                if (file != null)
                {
                    string fileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product"); //Tạo đường dẫn @wwwRootPath\images\product

                    if (!string.IsNullOrEmpty(productObj.Product!.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productObj.Product.ImageUrl!.TrimStart('\\'));//Xóa kí tự \ đầu chuỗi cũ và nối lại 
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);//Xóa file cũ trong wwwroot\images\product
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);//Thêm tệp vào fileStream
                    }
                    productObj.Product!.ImageUrl = @"\images\product\" + fileName;
                }
                if (productObj.Product!.Id == 0)
                {
                    _unitOfWork.Product.Add(productObj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Added Successfully";
                }
                else
                {
                    _unitOfWork.Product.Update(productObj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product Updated Successfully";
                }
                return RedirectToAction("Index");
            }
            else
            {
                productObj.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productObj);
            }
        }
        #endregion
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() { 
            var objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category");
            return Json(new {data= objProductList});
        }
        [HttpDelete]
		public IActionResult Delete(int? id)
		{
			var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
			if (obj == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			} 
            if (obj!.ImageUrl != null)
            {
				var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl!.TrimStart('\\'));
				if (System.IO.File.Exists(oldImagePath))
				{
					System.IO.File.Delete(oldImagePath);
				}
			}
			_unitOfWork.Product.Remove(obj);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Deleted Successfully" });
		}
		#endregion
	}
}
