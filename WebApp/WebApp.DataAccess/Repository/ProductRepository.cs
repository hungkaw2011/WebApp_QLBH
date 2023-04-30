using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.DataAccess.Repository.IRepository;
using WebApp.Models;

namespace WebApp.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        ///             Change Tracking
        ///     Cơ chế giúp theo dõi sự thay đổi của từng object do Entity Framework đang quản lý trong bộ nhớ.
        ///     Việc theo dõi giúp Entity Framework sinh ra những truy vấn phù hợp để thực thi trên cơ sở dữ liệu.
        ///     Nhờ cơ chế này, việc cập nhật (thêm/sửa/xóa) object từ client code được đơn giản hóa
        /// </summary>
        /// <param name="obj"></param>
        public void Update(Product obj)
        {
            var productFromDb =_db.Products.FirstOrDefault(u=>u.Id== obj.Id);
            //var productFromDb =_db.Products.AsNoTracking().FirstOrDefault(u=>u.Id== obj.Id);
            if (productFromDb != null)
            {
                productFromDb.ISBN=obj.ISBN;
                productFromDb.Price=obj.Price;
                productFromDb.Author=obj.Author;
                productFromDb.Description=obj.Description;
                productFromDb.Title=obj.Title;
                productFromDb.ListPrice = obj.ListPrice;
                productFromDb.Price50=obj.Price50;
                productFromDb.Price100=obj.Price100;

                if (obj.ImageUrl!=null)
                {
                    productFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
